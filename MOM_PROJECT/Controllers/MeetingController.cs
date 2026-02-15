using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MOM_PROJECT.Controllers;

public class MeetingController : Controller
{
    // GET
    public IActionResult MeetingList()
    {
        List<MeetingModel> list = new List<MeetingModel>();

        string connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        SqlConnection con = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "PR_MOM_Meetings_SelectAll";
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            MeetingModel Meeting = new MeetingModel();

            Meeting.CancellationReason =
                reader["CancellationReason"] != DBNull.Value
                ? reader["CancellationReason"].ToString()
                : null;

            Meeting.DocumentPath =
                reader["DocumentPath"] != DBNull.Value
                ? reader["DocumentPath"].ToString()
                : null;

            Meeting.MeetingDescription =
                reader["MeetingDescription"] != DBNull.Value
                ? reader["MeetingDescription"].ToString()
                : null;

            Meeting.IsCancelled =
                reader["IsCancelled"] != DBNull.Value
                ? Convert.ToBoolean(reader["IsCancelled"])
                : false;

            Meeting.CancellationDateTime =
                reader["CancellationDateTime"] != DBNull.Value
                ? Convert.ToDateTime(reader["CancellationDateTime"])
                : null;

            list.Add(Meeting);
        }

        reader.Close();
        con.Close();

        return View(list);
    }

    public IActionResult MeetingAddEdit()
    {
        return View();
    }

    public IActionResult MeetingSave(MeetingModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("MeetingAddEdit", model);
        }

        return RedirectToAction("MeetingList");
    }
}