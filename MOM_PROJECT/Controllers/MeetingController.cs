using Microsoft.AspNetCore.Mvc;

namespace MOM_PROJECT.Controllers;

public class MeetingController : Controller
{
    // GET
    public IActionResult MeetingList()
    {
        return View();
    }
    public IActionResult MeetingAddEdit()
    {
        return View();
    }
}