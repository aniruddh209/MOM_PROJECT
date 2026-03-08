using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MOM_PROJECT.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly string _connectionString =
            // "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";
            "Server=localhost,1433;Database=MOM_PROJECT;User Id=sa;Password=Aniruddh18;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        public IActionResult DepartmentList()
        {
            List<DepartmentModel> list = new List<DepartmentModel>();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_MOM_Department_SelectAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DepartmentModel model = new DepartmentModel();
                model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                model.DepartmentName = reader["DepartmentName"].ToString();
                list.Add(model);
            }

            return View(list);
        }
        
        [HttpGet]
        public IActionResult DepartmentAddEdit(int? id)
        {
            DepartmentModel model = new DepartmentModel();

            if (id.HasValue && id > 0)
            {
                using SqlConnection con = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("PR_MOM_Department_SelectByPK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", id.Value);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                    model.DepartmentName = reader["DepartmentName"].ToString();
                }
            }

            return View(model);
        }
        
        [HttpPost]
        public IActionResult DepartmentAddEdit(DepartmentModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            if (model.DepartmentID > 0)
            {
                cmd.CommandText = "PR_MOM_Department_UpdateByPK";
                cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
            }
            else
            {
                cmd.CommandText = "PR_MOM_Department_Insert";
            }

            cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);

            con.Open();
            cmd.ExecuteNonQuery();

            TempData["SuccessMessage"] =
                model.DepartmentID > 0
                    ? "Department updated successfully."
                    : "Department added successfully.";

            return RedirectToAction("DepartmentList");
        }
        
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                using SqlConnection con = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("PR_MOM_Department_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", id);

                con.Open();
                cmd.ExecuteNonQuery();

                TempData["SuccessMessage"] = "Department deleted successfully.";
            }
            catch (SqlException)
            {
                TempData["ErrorMessage"] =
                    "Cannot delete department. Staff exists under this department.";
            }

            return RedirectToAction("DepartmentList");
        }
    }
}