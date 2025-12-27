
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Login");

            ViewBag.Token = token;
            return View();
        }

    }
}
