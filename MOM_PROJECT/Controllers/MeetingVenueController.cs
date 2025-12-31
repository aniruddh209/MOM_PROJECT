using Microsoft.AspNetCore.Mvc;

namespace MOM_PROJECT.Controllers;

public class MeetingVenueController : Controller
{
    // GET
    public IActionResult MeetingVenueAddEdit()
    {
        return View();
    }
    public IActionResult MeetingVenueList()
    {
        return View();
    }
}