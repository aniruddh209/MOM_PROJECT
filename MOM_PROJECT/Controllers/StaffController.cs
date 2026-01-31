using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
namespace MOM_PROJECT.Controllers;
using Microsoft.Data.SqlClient;
using System.Data;

    
public class StaffController : Controller
{
    // GET
    public IActionResult StaffList()
    {
        //Connection string
        List<StaffModel> list = new List<StaffModel>();

        SqlConnection con = new SqlConnection(
            "Server=localhost;Database=MOM_PROJECT;User Id=sa;Password=Aniruddh18;TrustServerCertificate=True;"
        );
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "PR_MOM_Staff_SelectAll";
        cmd.CommandType = CommandType.StoredProcedure;
        //execute command
        con.Open();
        
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            StaffModel staff = new StaffModel();
            staff.StaffID = Convert.ToInt32(reader["StaffId"]);
            staff.StaffName= Convert.ToString(reader["StaffName"]);
            staff.MobileNo = Convert.ToString(reader["MobileNo"]);
            staff.EmailAddress = Convert.ToString(reader["EmailAddress"]);
            staff.Remarks = Convert.ToString(reader["Remarks"]);

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
    public IActionResult StaffSave(StaffModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("StaffAddEdit", model);
        }

        return RedirectToAction("StaffList");
    }
}

