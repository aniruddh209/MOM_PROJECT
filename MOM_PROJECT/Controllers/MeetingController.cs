using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MOM_PROJECT.Controllers
{
    public class MeetingController : Controller
    {
        private readonly string _connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        public IActionResult MeetingList()
        {
            List<MeetingModel> list = new List<MeetingModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
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
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult MeetingAddEdit(int id = 0)
        {
            MeetingModel model = new MeetingModel();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                if (id > 0)
                {
                    SqlCommand cmd = new SqlCommand("PR_MOM_Meetings_SelectByPK", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MeetingID", id);

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

                    reader.Close();
                }
                model.DepartmentList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                model.DepartmentList = new List<SelectListItem>();

                SqlCommand deptCmd = new SqlCommand("PR_MOM_Department_SelectAll", con);
                deptCmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader deptReader = deptCmd.ExecuteReader();

                while (deptReader.Read())
                {
                    model.DepartmentList.Add(new SelectListItem
                    {
                        Value = deptReader["DepartmentID"].ToString(),
                        Text = deptReader["DepartmentName"].ToString()
                    });
                }

                deptReader.Close();

                model.MeetingTypeList = new List<SelectListItem>();
                SqlCommand typeCmd = new SqlCommand("PR_MOM_MeetingType_SelectAll", con);
                typeCmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader typeReader = typeCmd.ExecuteReader();

                while (typeReader.Read())
                {
                    model.MeetingTypeList.Add(new SelectListItem
                    {
                        Value = typeReader["MeetingTypeID"].ToString(),
                        Text = typeReader["MeetingTypeName"].ToString()
                    });
                }
                typeReader.Close();

                model.MeetingVenueList = new List<SelectListItem>();
                SqlCommand venueCmd = new SqlCommand("PR_MOM_MeetingVenue_SelectAll", con);
                venueCmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader venueReader = venueCmd.ExecuteReader();

                while (venueReader.Read())
                {
                    model.MeetingVenueList.Add(new SelectListItem
                    {
                        Value = venueReader["MeetingVenueID"].ToString(),
                        Text = venueReader["MeetingVenueName"].ToString()
                    });
                }
                venueReader.Close();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult MeetingAddEdit(MeetingModel model)
        {
            if (!ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    model.DepartmentList = new List<SelectListItem>();
                    SqlCommand deptCmd = new SqlCommand("PR_MOM_Department_SelectAll", con);
                    deptCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader deptReader = deptCmd.ExecuteReader();
                    while (deptReader.Read())
                    {
                        model.DepartmentList.Add(new SelectListItem
                        {
                            Value = deptReader["DepartmentID"].ToString(),
                            Text = deptReader["DepartmentName"].ToString()
                        });
                    }
                    deptReader.Close();

                    model.MeetingTypeList = new List<SelectListItem>();
                    SqlCommand typeCmd = new SqlCommand("PR_MOM_MeetingType_SelectAll", con);
                    typeCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader typeReader = typeCmd.ExecuteReader();
                    while (typeReader.Read())
                    {
                        model.MeetingTypeList.Add(new SelectListItem
                        {
                            Value = typeReader["MeetingTypeID"].ToString(),
                            Text = typeReader["MeetingTypeName"].ToString()
                        });
                    }
                    typeReader.Close();

                    model.MeetingVenueList = new List<SelectListItem>();
                    SqlCommand venueCmd = new SqlCommand("PR_MOM_MeetingVenue_SelectAll", con);
                    venueCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader venueReader = venueCmd.ExecuteReader();
                    while (venueReader.Read())
                    {
                        model.MeetingVenueList.Add(new SelectListItem
                        {
                            Value = venueReader["MeetingVenueID"].ToString(),
                            Text = venueReader["MeetingVenueName"].ToString()
                        });
                    }
                    venueReader.Close();
                }
                return View("MeetingAddEdit", model);
            }

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
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
            }

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
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    // Delete associated meeting members first to avoid FK constraints
                    SqlCommand deleteMembersCmd = new SqlCommand("DELETE FROM MOM_MeetingMember WHERE MeetingID = @MeetingID", con);
                    deleteMembersCmd.Parameters.AddWithValue("@MeetingID", id);
                    deleteMembersCmd.ExecuteNonQuery();

                    // Now delete the meeting itself
                    SqlCommand cmd = new SqlCommand("PR_MOM_Meetings_DeleteByPK", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MeetingID", id);

                    cmd.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Meeting and its members deleted successfully.";
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