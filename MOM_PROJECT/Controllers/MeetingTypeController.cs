using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
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
    public IActionResult MeetingTypeSave(MeetingTypeModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("MeetingTypeAddEdit", model);
        }

        return RedirectToAction("MeetingTypeList");
    }
}