using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MOM_PROJECT.Models;
using System.Data;
using System.Collections.Generic;

namespace MOM_PROJECT.Controllers
{
    public class MeetingMemberController : Controller
    {
        private readonly string _connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";
        
        public IActionResult MeetingMemberList()
        {
            List<MeetingMemberModel> list = new List<MeetingMemberModel>();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_MOM_MeetingMember_SelectAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new MeetingMemberModel
                {
                    MeetingMemberID = Convert.ToInt32(reader["MeetingMemberID"]),
                    MeetingID = Convert.ToInt32(reader["MeetingID"]),
                    StaffID = Convert.ToInt32(reader["StaffID"]),
                    IsPresent = Convert.ToBoolean(reader["IsPresent"]),
                    Remarks = reader["Remarks"]?.ToString()
                });
            }

            return View(list);
        }
        
        [HttpGet]
        public IActionResult MeetingMemberAddEdit(int id = 0)
        {
            MeetingMemberModel model = new MeetingMemberModel();

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            // EDIT
            if (id > 0)
            {
                using SqlCommand cmd = new SqlCommand("PR_MOM_MeetingMember_SelectByPK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingMemberID", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    model.MeetingMemberID = id;
                    model.MeetingID = Convert.ToInt32(reader["MeetingID"]);
                    model.StaffID = Convert.ToInt32(reader["StaffID"]);
                    model.IsPresent = Convert.ToBoolean(reader["IsPresent"]);
                    model.Remarks = reader["Remarks"]?.ToString();
                }
                reader.Close();
            }

            // MEETING LIST
            model.MeetingList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            using (SqlCommand cmd = new SqlCommand("PR_MOM_Meetings_SelectAll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    model.MeetingList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = reader["MeetingID"].ToString(),
                        Text = reader["MeetingDescription"].ToString()
                    });
                }
                reader.Close();
            }

            // STAFF LIST
            model.StaffList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            using (SqlCommand cmd = new SqlCommand("PR_MOM_Staff_SelectAll", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    model.StaffList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = reader["StaffID"].ToString(),
                        Text = reader["StaffName"].ToString()
                    });
                }
            }

            return View(model);
        }
        
        [HttpPost]
        public IActionResult MeetingMemberSave(MeetingMemberModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("MeetingMemberAddEdit", model);
            }

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            if (model.MeetingMemberID > 0)
            {
                cmd.CommandText = "PR_MOM_MeetingMember_UpdateByPK";
                cmd.Parameters.AddWithValue("@MeetingMemberID", model.MeetingMemberID);
                cmd.Parameters.AddWithValue("@IsPresent", model.IsPresent);
                cmd.Parameters.AddWithValue("@Remarks", model.Remarks ?? "");

                TempData["SuccessMessage"] = "Meeting member updated successfully.";
            }
            else
            {
                cmd.CommandText = "PR_MOM_MeetingMember_Insert";
                cmd.Parameters.AddWithValue("@MeetingID", model.MeetingID);
                cmd.Parameters.AddWithValue("@StaffID", model.StaffID);
                cmd.Parameters.AddWithValue("@IsPresent", model.IsPresent);
                cmd.Parameters.AddWithValue("@Remarks", model.Remarks ?? "");

                TempData["SuccessMessage"] = "Meeting member added successfully.";
            }

            con.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("MeetingMemberList");
        }
        
        public IActionResult DeleteMeetingMember(int id)
        {
            try
            {
                using SqlConnection con = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("PR_MOM_MeetingMember_DeleteByPK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MeetingMemberID", id);

                con.Open();
                cmd.ExecuteNonQuery();

                TempData["SuccessMessage"] = "Meeting member deleted successfully.";
            }
            catch (SqlException)
            {
                TempData["ErrorMessage"] =
                    "This meeting member cannot be deleted because it is linked with other records.";
            }

            return RedirectToAction("MeetingMemberList");
        }
    }
}