using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MOM_PROJECT.Controllers
{
    public class MeetingController : Controller
    {
        private readonly string _connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        public IActionResult MeetingList()
        {
            List<MeetingModel> list = new List<MeetingModel>();

            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("PR_MOM_Meetings_SelectAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MeetingModel model = new MeetingModel();

                model.MeetingID = Convert.ToInt32(reader["MeetingID"]);
                model.MeetingDate = reader["MeetingDate"] as DateTime?;
                model.MeetingDescription = reader["MeetingDescription"]?.ToString();
                model.DocumentPath = reader["DocumentPath"]?.ToString();

                if (reader["IsCancelled"] != DBNull.Value)
                    model.IsCancelled = Convert.ToBoolean(reader["IsCancelled"]);

                model.CancellationDateTime = reader["CancellationDateTime"] as DateTime?;
                model.CancellationReason = reader["CancellationReason"]?.ToString();

                list.Add(model);
            }

            con.Close();
            return View(list);
        }

        [HttpGet]
        public IActionResult MeetingAddEdit(int id = 0)
        {
            MeetingModel model = new MeetingModel();

            if (id > 0)
            {
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand("PR_MOM_Meetings_SelectByPK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingID", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model.MeetingID = id;
                    model.MeetingDate = reader["MeetingDate"] as DateTime?;
                    model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                    model.MeetingTypeID = Convert.ToInt32(reader["MeetingTypeID"]);
                    model.MeetingVenueID = Convert.ToInt32(reader["MeetingVenueID"]);
                    model.MeetingDescription = reader["MeetingDescription"]?.ToString();
                    model.DocumentPath = reader["DocumentPath"]?.ToString();
                    model.IsCancelled = Convert.ToBoolean(reader["IsCancelled"]);
                    model.CancellationDateTime = reader["CancellationDateTime"] as DateTime?;
                    model.CancellationReason = reader["CancellationReason"]?.ToString();
                }

                con.Close();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult MeetingAddEdit(MeetingModel model)
        {
            if (!ModelState.IsValid)
                return View("MeetingAddEdit", model);

            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            if (model.MeetingID > 0)
            {
                cmd.CommandText = "PR_MOM_Meetings_UpdateByPK";
                cmd.Parameters.AddWithValue("@MeetingID", model.MeetingID);
                cmd.Parameters.AddWithValue("@IsCancelled", model.IsCancelled);
                cmd.Parameters.AddWithValue("@CancellationDateTime",
                    model.CancellationDateTime ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CancellationReason",
                    model.CancellationReason ?? (object)DBNull.Value);
            }
            else
            {
                cmd.CommandText = "PR_MOM_Meetings_Insert";
            }

            cmd.Parameters.AddWithValue("@MeetingDate", model.MeetingDate);
            cmd.Parameters.AddWithValue("@MeetingVenueID", model.MeetingVenueID);
            cmd.Parameters.AddWithValue("@MeetingTypeID", model.MeetingTypeID);
            cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
            cmd.Parameters.AddWithValue("@MeetingDescription", model.MeetingDescription);
            cmd.Parameters.AddWithValue("@DocumentPath",
                model.DocumentPath ?? (object)DBNull.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            TempData["SuccessMessage"] =
                model.MeetingID == 0
                    ? "Meeting added successfully."
                    : "Meeting updated successfully.";

            return RedirectToAction("MeetingList");
        }

        public IActionResult DeleteMeeting(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand("PR_MOM_Meetings_DeleteByPK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                TempData["SuccessMessage"] = "Meeting deleted successfully.";
            }
            catch (SqlException)
            {
                TempData["ErrorMessage"] =
                    "This meeting cannot be deleted because it is linked with other records.";
            }

            return RedirectToAction("MeetingList");
        }
    }
}