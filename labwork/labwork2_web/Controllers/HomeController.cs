using labwork2_web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using teploobmenLibrary;

namespace labwork2_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(TeploobmenInput input)
        {
            var lib = new Teploobmen(input);
            var resualt = lib.calc();
            return View(resualt);
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}