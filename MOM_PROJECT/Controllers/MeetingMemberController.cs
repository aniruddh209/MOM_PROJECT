using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MOM_PROJECT.Controllers;

public class MeetingMemberController : Controller
{
    // GET
    public IActionResult MeetingMemberList()
    {
        List<MeetingMemberModel> list = new List<MeetingMemberModel>();

        string connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        SqlConnection con = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "PR_MOM_MeetingMember_SelectAll";
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            MeetingMemberModel MeetingMember = new MeetingMemberModel();

            MeetingMember.MeetingMemberID = Convert.ToInt32(reader["MeetingMemberID"]);
            MeetingMember.IsPresent =
                reader["IsPresent"] != DBNull.Value
                    ? Convert.ToBoolean(reader["IsPresent"])
                    : false;
            MeetingMember.Remarks = reader["Remarks"].ToString();
            

            list.Add(MeetingMember);
        }

        reader.Close();
        con.Close();

        return View(list);
    }
    public IActionResult MeetingMemberAddEdit()
    {
        return View();
    }
    public IActionResult MeetingMemberSave(MeetingMemberModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("MeetingMemberAddEdit", model);
        }

        return RedirectToAction("MeetingMemberList");
    }
}