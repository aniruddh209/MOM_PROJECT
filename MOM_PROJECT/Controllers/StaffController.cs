using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MOM_PROJECT.Controllers
{
    public class StaffController : Controller
    {
        // GET
        public IActionResult StaffList()
        {
            List<StaffModel> list = new List<StaffModel>();

            string connectionString =
                "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "PR_MOM_Staff_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                StaffModel staff = new StaffModel();

                // staff.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                // staff.DepartmentName = reader["DepartmentName"].ToString();
                // staff.StaffID = Convert.ToInt32(reader["StaffID"]);
                staff.StaffName = reader["StaffName"].ToString();
                staff.EmailAddress = reader["EmailAddress"].ToString();
                staff.MobileNo = reader["MobileNo"].ToString();
                staff.Remarks = reader["Remarks"].ToString();
                // staff.Created = Convert.ToDateTime(reader["Created"]);
                // staff.Modified = Convert.ToDateTime(reader["Modified"]);

                list.Add(staff);
            }

            reader.Close();
            con.Close();

            return View(list);
        }

        public IActionResult StaffAddEdit()
        {
            return View();
        }

        public IActionResult MeetingSave(StaffModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("StaffAddEdit", model);
            }

            return RedirectToAction("StaffList");
        }
    }
}