using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class ContactController : Controller

    {
        [HttpPost]
        public IActionResult SendMessage(string name, string email, string subject, string message)
        {
            return RedirectToAction("Confirmation");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }

    }
}
