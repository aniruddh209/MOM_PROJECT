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
    
}