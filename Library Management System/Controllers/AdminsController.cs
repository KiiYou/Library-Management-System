using Library_Management_System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class AdminsController : Controller
    {
        private readonly LibraryContext _context;

        public AdminsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admins
                .FirstOrDefault(a => a.Username == username && a.Password == password);

            if (admin != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        [HttpGet]
        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword(string username, string oldPassword, string newPassword)
        {
            
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username && a.Password == oldPassword);

            if (admin != null)
            {
                
                admin.Password = newPassword;

                
                _context.SaveChanges();

                
                return RedirectToAction("Login", "Admins"); 
            }

            
            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }



    }
}
