using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult CantAttend()
        {
            return View();
        }

        public IActionResult Attended()
        {
            return View();
        }
        public IActionResult AttendedTest()
        {
            return View();
        }

        public IActionResult NotFound(int code)
        {
            ViewBag.Code = code;
            return View();
        }
    }
}
