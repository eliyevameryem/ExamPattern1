
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace exam2.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}