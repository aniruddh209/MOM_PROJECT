using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
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
    public IActionResult MeetingSave(MeetingModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("MeetingAddEdit", model);
        }

        return RedirectToAction("MeetingList");
    }
}