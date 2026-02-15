using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
namespace MOM_PROJECT.Controllers;

public class DepartmentController : Controller
{
    // GET
    public IActionResult DepartmentAddEdit()
    {
        return View();
    }
    public IActionResult DepartmentList()
    {
        List<DepartmentModel> list = new List<DepartmentModel>();

        string connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        SqlConnection con = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "PR_MOM_Department_SelectAll";
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            DepartmentModel department = new DepartmentModel();

            // staff.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
            // staff.DepartmentName = reader["DepartmentName"].ToString();
            // staff.StaffID = Convert.ToInt32(reader["StaffID"]);
            department.DepartmentName = reader["DepartmentName"].ToString();
            department.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
            // staff.Created = Convert.ToDateTime(reader["Created"]);
            // staff.Modified = Convert.ToDateTime(reader["Modified"]);

            list.Add(department);
        }

        reader.Close();
        con.Close();

        return View(list);
    }
    public IActionResult Departmentsave(DepartmentModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("DepartmentAddEdit", model);
        }

        return RedirectToAction("DepartmentList");
    }
}