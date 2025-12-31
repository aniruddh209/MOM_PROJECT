using Microsoft.AspNetCore.Mvc;

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
}