using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MOM_PROJECT.Controllers
{
    public class StaffController : Controller
    {
        private readonly string _connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        public IActionResult StaffList()
        {
            List<StaffModel> list = new List<StaffModel>();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_MOM_Staff_SelectAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                StaffModel model = new StaffModel();
                model.StaffID = Convert.ToInt32(reader["StaffID"]);
                model.StaffName = reader["StaffName"].ToString();
                model.MobileNo = reader["MobileNo"].ToString();
                model.EmailAddress = reader["EmailAddress"].ToString();
                model.Remarks = reader["Remarks"]?.ToString();
                model.DepartmentName = reader["DepartmentName"].ToString();
                list.Add(model);
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult StaffAddEdit(int id = 0)
        {
            StaffModel model = new StaffModel();

            using SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            if (id > 0)
            {
                using SqlCommand cmd = new SqlCommand("PR_MOM_Staff_SelectByPK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffID", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    model.StaffID = id;
                    model.StaffName = reader["StaffName"].ToString();
                    model.MobileNo = reader["MobileNo"].ToString();
                    model.EmailAddress = reader["EmailAddress"].ToString();
                    model.Remarks = reader["Remarks"]?.ToString();
                    model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                }
                reader.Close();
            }

            model.DepartmentList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            using SqlCommand deptCmd = new SqlCommand("PR_MOM_Department_SelectAll", con);
            deptCmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader deptReader = deptCmd.ExecuteReader();
            while (deptReader.Read())
            {
                model.DepartmentList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = deptReader["DepartmentID"].ToString(),
                    Text = deptReader["DepartmentName"].ToString()
                });
            }
            deptReader.Close();

            return View(model);
        }

        [HttpPost]
        public IActionResult StaffAddEdit(StaffModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using SqlConnection con = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                if (model.StaffID == 0)
                {
                    cmd.CommandText = "PR_MOM_Staff_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_MOM_Staff_UpdateByPK";
                    cmd.Parameters.AddWithValue("@StaffID", model.StaffID);
                }

                cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                cmd.Parameters.AddWithValue("@StaffName", model.StaffName);
                cmd.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                cmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
                cmd.Parameters.AddWithValue("@Remarks", (object?)model.Remarks ?? DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();

                TempData["SuccessMessage"] =
                    model.StaffID == 0
                        ? "Staff added successfully."
                        : "Staff updated successfully.";

                return RedirectToAction("StaffList");
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("UQ__MOM_Staf") || ex.Message.Contains("UNIQUE"))
                {
                    TempData["ErrorMessage"] =
                        "This email address already exists. Please use another email.";
                }
                else
                {
                    TempData["ErrorMessage"] =
                        "An error occurred while saving staff details.";
                }

                return RedirectToAction("StaffAddEdit", new { id = model.StaffID });
            }
        }

        public IActionResult DeleteStaff(int id)
        {
            try
            {
                using SqlConnection con = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("PR_MOM_Staff_DeleteByPK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffID", id);

                con.Open();
                cmd.ExecuteNonQuery();

                TempData["SuccessMessage"] = "Staff deleted successfully.";
            }
            catch
            {
                TempData["ErrorMessage"] =
                    "Cannot delete staff (linked records exist).";
            }

            return RedirectToAction("StaffList");
        }
    }
}