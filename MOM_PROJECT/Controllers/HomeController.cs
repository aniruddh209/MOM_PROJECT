using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MOM_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString =
            "Server=localhost;Database=MOM_PROJECT;User Id=SA;Password=Aniruddh18;TrustServerCertificate=True;";

        public IActionResult Index()
        {
            int totalMeetings   = 0;
            int cancelled       = 0;
            int totalMembers    = 0;
            int totalDepts      = 0;

            List<string> typeLabels = new List<string>();
            List<int> typeData = new List<int>();
            List<string> deptLabels = new List<string>();
            List<int> deptData = new List<int>();

            try
            {
                using SqlConnection con = new SqlConnection(_connectionString);
                con.Open();

                // ── Total meetings ─────────────────────────────
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MOM_Meetings", con))
                {
                    totalMeetings = (int)cmd.ExecuteScalar();
                }

                // ── Cancelled meetings ─────────────────────────
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MOM_Meetings WHERE IsCancelled = 1", con))
                {
                    cancelled = (int)cmd.ExecuteScalar();
                }

                // ── Total meeting members (attendance records) ─
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MOM_MeetingMember", con))
                {
                    totalMembers = (int)cmd.ExecuteScalar();
                }

                // ── Total departments ──────────────────────────
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MOM_Department", con))
                {
                    totalDepts = (int)cmd.ExecuteScalar();
                }

                // ── dynamic charts: Meetings by Type ───────────
                string typeQry = @"
                    SELECT t.MeetingTypeName, COUNT(m.MeetingID) 
                    FROM MOM_MeetingType t 
                    LEFT JOIN MOM_Meetings m ON t.MeetingTypeID = m.MeetingTypeID 
                    GROUP BY t.MeetingTypeName";
                using (SqlCommand cmd = new SqlCommand(typeQry, con))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        typeLabels.Add(dr.GetString(0));
                        typeData.Add(dr.IsDBNull(1) ? 0 : dr.GetInt32(1));
                    }
                }

                // ── dynamic charts: Meetings by Department ──────
                string deptQry = @"
                    SELECT d.DepartmentName, COUNT(m.MeetingID) 
                    FROM MOM_Department d 
                    LEFT JOIN MOM_Meetings m ON d.DepartmentID = m.DepartmentID 
                    GROUP BY d.DepartmentName";
                using (SqlCommand cmd = new SqlCommand(deptQry, con))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        deptLabels.Add(dr.GetString(0));
                        deptData.Add(dr.IsDBNull(1) ? 0 : dr.GetInt32(1));
                    }
                }
            }
            catch
            {
                // If DB is down, fall back to zeros — no crash
            }

            int completed = totalMeetings - cancelled;

            ViewBag.TotalMeetings    = totalMeetings;
            ViewBag.CancelledMeetings = cancelled;
            ViewBag.CompletedMeetings = completed;
            ViewBag.TotalMembers      = totalMembers;
            ViewBag.TotalDepts        = totalDepts;

            ViewBag.TypeLabels = typeLabels.ToArray();
            ViewBag.TypeData = typeData.ToArray();
            ViewBag.DeptLabels = deptLabels.ToArray();
            ViewBag.DeptData = deptData.ToArray();

            return View();
        }
    }
}