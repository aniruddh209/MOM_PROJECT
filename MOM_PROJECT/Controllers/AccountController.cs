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

    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    private static string fullName = "Aniruddh Parmar";
    private static string company = "MOM IT Solutions";
    private static string job = "Lead Administrator";
    private static string country = "India";
    private static string phone = "+91 98765 43210";
    private static string email = "aniruddh@test.com";

    public ActionResult Profile()
    {
        ViewBag.FullName = fullName;
        ViewBag.Company = company;
        ViewBag.Job = job;
        ViewBag.Country = country;
        ViewBag.Phone = phone;
        ViewBag.Email = email;
        return View();
    }

    [HttpPost]
    public ActionResult UpdateProfile(string fullNameInput, string companyInput, string jobInput, string countryInput, string phoneInput, string emailInput)
    {
        if(!string.IsNullOrEmpty(fullNameInput)) fullName = fullNameInput;
        if(!string.IsNullOrEmpty(companyInput)) company = companyInput;
        if(!string.IsNullOrEmpty(jobInput)) job = jobInput;
        if(!string.IsNullOrEmpty(countryInput)) country = countryInput;
        if(!string.IsNullOrEmpty(phoneInput)) phone = phoneInput;
        if(!string.IsNullOrEmpty(emailInput)) email = emailInput;

        TempData["SuccessMessage"] = "Profile updated successfully!";
        return RedirectToAction("Profile");
    }
}