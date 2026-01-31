using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
namespace MOM_PROJECT.Controllers;

public class StaffController : Controller
{
    // GET
    public IActionResult StaffList()
    {
        return View();
    }
    public IActionResult StaffAddEdit()
    {
        return View();
    }
    public IActionResult StaffSave(StaffModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("StaffAddEdit", model);
        }

        return RedirectToAction("StaffList");
    }
}