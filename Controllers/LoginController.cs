using Microsoft.AspNetCore.Mvc;
using SurveyApp.Models;

namespace SurveyApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult AdminLogin()
        {
            return View(new PasswordModel());
        }

        [HttpPost]
        public IActionResult AdminLogin(PasswordModel model)
        {
            string password = "surveydonky123";
            if (model.Password == password)
            {
                return RedirectToAction("SurveyList", "Admin", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("AdminLogin", "Login");
            }
        }
        public IActionResult CantAccess()
        {
            return View();
        }
    }
}
