using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MOM_PROJECT.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult DepartmentAddEdit()
        {
            return View();
        }

        public IActionResult DepartmentList()
        {
            List<DepartmentModel> list = new List<DepartmentModel>();

            SqlConnection con = new SqlConnection(
                "Server=localhost;Database=MOM_PROJECT;User Id=sa;Password=Aniruddh18;TrustServerCertificate=True;"
            );
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "PR_MOM_Department_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;
            //execute command
            con.Open();
        
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            { 
                DepartmentModel department = new DepartmentModel();
                department.DepartmentName = Convert.ToString(reader["DepartmentName"]);
                list.Add(department);
            }

            reader.Close();
            con.Close();
        
            return View(list);
            return View();
        }

        [HttpPost]
        public IActionResult DepartmentSave(DepartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmentAddEdit", model);
            }
            

            return RedirectToAction("DepartmentList");
        }
    }
}