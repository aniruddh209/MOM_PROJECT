using Microsoft.AspNetCore.Mvc;

namespace MOM_PROJECT.Controllers;

public class MeetingMemberController : Controller
{
    // GET
    public IActionResult MeetingMemberList()
    {
        return View();
    }
    public IActionResult MeetingMemberAddEdit()
    {
        return View();
    }
}