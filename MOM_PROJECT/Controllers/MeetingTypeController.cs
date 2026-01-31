using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
using Microsoft.Data.SqlClient;
using System.Data;

public class MeetingTypeController : Controller
{
    public IActionResult MeetingTypeAddEdit()
    {
        return View();
    }
    public IActionResult MeetingTypeList()
    {
        List<MeetingTypeModel> list = new List<MeetingTypeModel>();

        SqlConnection con = new SqlConnection(
            "Server=localhost;Database=MOM_PROJECT;User Id=sa;Password=Aniruddh18;TrustServerCertificate=True;"
        );
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "PR_MOM_MeetingType_SelectAll";
        cmd.CommandType = CommandType.StoredProcedure;
        //execute command
        con.Open();
        
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        { 
            MeetingTypeModel meetingtype = new MeetingTypeModel();
            meetingtype.MeetingTypeName = Convert.ToString(reader["MeetingTypeName"]);
            meetingtype.Remarks= Convert.ToString(reader["Remarks"]);
            list.Add(meetingtype);
        }

        reader.Close();
        con.Close();
        
        return View(list);
    }
    public IActionResult MeetingTypeSave(MeetingTypeModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("MeetingTypeAddEdit", model);
        }

        return RedirectToAction("MeetingTypeList");
    }
}