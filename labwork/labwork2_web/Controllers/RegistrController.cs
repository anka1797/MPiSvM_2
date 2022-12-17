using labwork2_web.Data;
using labwork2_web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using teploobmenLibrary;

namespace labwork2_web.Controllers
{
    public class RegistrController : Controller
    {
        private readonly ILogger<RegistrController> _logger;
        private readonly ApplicationContext _context;

        public RegistrController(ILogger<RegistrController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _context = applicationContext;
        }

        
        public IActionResult Index(TeploobmenInput input)
        {
            if (!string.IsNullOrEmpty(input.Email))
            {
                
                var user = new User
                {
                    Email = input.Email,
                    Password = input.Password,
                    //NameUser = input.NameUser
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
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