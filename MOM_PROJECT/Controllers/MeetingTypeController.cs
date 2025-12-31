using Microsoft.AspNetCore.Mvc;

namespace MOM_PROJECT.Controllers;

public class MeetingTypeController : Controller
{
    public IActionResult MeetingTypeAddEdit()
    {
        return View();
    }
    public IActionResult MeetingTypeList()
    {
        return View();
    }
}