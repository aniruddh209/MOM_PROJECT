using Microsoft.AspNetCore.Mvc;

namespace MOM_PROJECT.Controllers;

public class DepartmentController : Controller
{
    // GET
    public IActionResult DepartmentAddEdit()
    {
        return View();
    }
    public IActionResult DepartmentList()
    {
        return View();
    }
}