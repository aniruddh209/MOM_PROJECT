using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
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

    public IActionResult MeetingMemberSave(MeetingModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("MeetingAddEdit", model);
        }

        return RedirectToAction("MeetingList");
    }
    
    // public IActionResult ContentDemo()
    // {
    //     return Content("This is Content Result from MeetingMemberController");
    // }
    //
    // public IActionResult JsonDemo()
    // {
    //     var member = new
    //     {
    //         Id = 1,
    //         Name = "Aniruddh",
    //         Role = "Member"
    //     };
    //
    //     return Json(member);
    // }
    //
    // public IActionResult RedirectDemo()
    // {
    //     return RedirectToAction("MeetingMemberList");
    // }
    //
    // public IActionResult StatusCodeDemo()
    // {
    //     return StatusCode(404);
    // }
}