using Microsoft.AspNetCore.Mvc;
using MOM_PROJECT.Data;
using MOM_PROJECT.Models;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace MOM_PROJECT.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ================= LOGIN =================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            string hash = HashPassword(password);

            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

        // ================= REGISTER =================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string Username, string Password, string Role)
        {
            if (_context.Users.Any(x => x.Username == Username))
            {
                ViewBag.Error = "Username already exists";
                return View();
            }

            var user = new UserModel
            {
                Username = Username,
                PasswordHash = HashPassword(Password),
                Role = Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ================= HASH =================
        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}