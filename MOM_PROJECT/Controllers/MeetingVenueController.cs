using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
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
    public IActionResult MeetingVenueSave(MeetingVenueModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("MeetingVenueAddEdit", model);
        }

        return RedirectToAction("MeetingVenueList");
    }
}