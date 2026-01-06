using Microsoft.AspNetCore.Mvc;
namespace MOM_PROJECT.Controllers;

    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }