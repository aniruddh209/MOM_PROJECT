using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
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

        SqlConnection con = new SqlConnection(
            "Server=localhost;Database=MOM_PROJECT;User Id=sa;Password=Aniruddh18;TrustServerCertificate=True;"
        );
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "PR_MOM_MeetingVenue_SelectAll";
        cmd.CommandType = CommandType.StoredProcedure;
        //execute command
        con.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            MeetingVenueModel meetingvenue = new MeetingVenueModel();
            meetingvenue.MeetingVenueID = Convert.ToInt32(reader["MeetingVenueID"]);
            meetingvenue.MeetingVenueName = Convert.ToString(reader["MeetingVenueName"]);
            meetingvenue.Created = Convert.ToDateTime(reader["Created"]);
            list.Add(meetingvenue);
        }

        reader.Close();
        con.Close();

        return View(list);
    }

    public IActionResult MeetingVenueSave(MeetingVenueModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("MeetingVenueAddEdit", model);
        }

        return RedirectToAction("MeetingVenueList");
    }
}