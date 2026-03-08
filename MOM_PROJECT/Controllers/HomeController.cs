using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MOM_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString =
            "Server=localhost,1433;Database=MOM_PROJECT;User Id=sa;Password=Aniruddh18;MultipleActiveResultSets=true;TrustServerCertificate=True;";

        public IActionResult Index()
        {
            int totalMeetings   = 0;
            int cancelled       = 0;
            int totalMembers    = 0;
            int totalDepts      = 0;

            try
            {
                using SqlConnection con = new SqlConnection(_connectionString);
                con.Open();

                // ── Total meetings ─────────────────────────────
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM MOM_Meetings", con))
                {
                    totalMeetings = (int)cmd.ExecuteScalar();
                }

                // ── Cancelled meetings ─────────────────────────
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM MOM_Meetings WHERE IsCancelled = 1", con))
                {
                    cancelled = (int)cmd.ExecuteScalar();
                }

                // ── Total meeting members (attendance records) ─
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM MOM_MeetingMembers", con))
                {
                    totalMembers = (int)cmd.ExecuteScalar();
                }

                // ── Total departments ──────────────────────────
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM MOM_Department", con))
                {
                    totalDepts = (int)cmd.ExecuteScalar();
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

            return View();
        }
    }
}