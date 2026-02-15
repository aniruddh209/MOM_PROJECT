using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace MOM_PROJECT.Controllers;
using Models;

public class MeetingTypeController : Controller
{
    public IActionResult MeetingTypeAddEdit()
    {
        return View();
    }
    public IActionResult MeetingTypeList()
    {
        List<MeetingTypeModel> list = new List<MeetingTypeModel>();

        string connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        SqlConnection con = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "PR_MOM_MeetingType_SelectAll";
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            MeetingTypeModel Meetingtype = new MeetingTypeModel();

            // staff.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
            // staff.DepartmentName = reader["DepartmentName"].ToString();
            // staff.StaffID = Convert.ToInt32(reader["StaffID"]);
            Meetingtype.MeetingTypeName = reader["MeetingTypeName"].ToString();
            Meetingtype.Remarks = reader["Remarks"].ToString();
            // staff.Created = Convert.ToDateTime(reader["Created"]);
            // staff.Modified = Convert.ToDateTime(reader["Modified"]);

            list.Add(Meetingtype);
        }

        reader.Close();
        con.Close();

        return View(list);
    }
    public IActionResult MeetingTypeSave(MeetingTypeModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("MeetingTypeAddEdit", model);
        }

        return RedirectToAction("MeetingTypeList");
    }
}