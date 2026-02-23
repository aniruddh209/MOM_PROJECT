using Microsoft.AspNetCore.Mvc;

namespace MOM_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // ===== STATIC DASHBOARD DATA =====
            ViewBag.TotalMeetings = 15;
            ViewBag.UpcomingMeetings = 0;
            ViewBag.CompletedMeetings = 10;
            ViewBag.CancelledMeetings = 5;

            ViewBag.TotalMeetingsChange = "+12% from last month";
            ViewBag.UpcomingChange = "+8% from last week";
            ViewBag.CompletedRate = "15% completion rate";
            ViewBag.CancelledRate = "3% cancellation rate";

            return View();
        }
    }
}