using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MOM_PROJECT.Controllers;
using Models;

public class MeetingVenueController : Controller
{
    // GET
    public IActionResult MeetingVenueAddEdit()
    {
        return View();
    }
    public IActionResult MeetingVenueList()
    {
        List<MeetingVenueModel> list = new List<MeetingVenueModel>();

        string connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        SqlConnection con = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "PR_MOM_MeetingVenue_SelectAll";
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            MeetingVenueModel MeetingVenue = new MeetingVenueModel();
            
            MeetingVenue.MeetingVenueID = Convert.ToInt32(reader["MeetingVenueID"]);
            MeetingVenue.MeetingVenueName = reader["MeetingVenueName"].ToString();
            MeetingVenue.Created = Convert.ToDateTime(reader["Created"]);

            list.Add(MeetingVenue);
        }

        reader.Close();
        con.Close();

        return View(list);
    }
    public IActionResult MeetingSave(MeetingVenueModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("MeetingVenueAddEdit", model);
        }

        return RedirectToAction("MeetingVenueList");
    }
}