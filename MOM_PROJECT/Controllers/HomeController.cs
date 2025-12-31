using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Models;

namespace MOM_PROJECT.Controllers;
public class HomeController : Controller
{
  

    public IActionResult Index()
    {
        return View();
    }
}