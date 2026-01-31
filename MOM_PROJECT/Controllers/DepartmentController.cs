using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;
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
    public IActionResult DepartmentSave(DepartmentModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("DepartmentAddEdit", model);
        }

        return RedirectToAction("DepartmentList");
    }
}