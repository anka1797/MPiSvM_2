using labwork2_web.Data;
using labwork2_web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using teploobmenLibrary;

namespace labwork2_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        private int _userId;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _context = applicationContext; 
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int.TryParse(User.FindFirst("Id")?.Value, out _userId);
        }

        [HttpPost]
        public IActionResult Result(TeploobmenInput input)
        {
            //Сохранение варианта
            if (!string.IsNullOrEmpty(input.Name))
            {
                var existVariant = _context.Variants.FirstOrDefault(x => x.Name == input.Name);

                if (existVariant != null)
                {
                    // Обновление варианта
                    existVariant.H = input.H ;
                    existVariant.t1 = input.t1;
                    existVariant.T = input.T ;
                    existVariant.w = input.w ;
                    existVariant.C = input.C ;
                    existVariant.Gm = input.Gm;
                    existVariant.Cm = input.Cm;
                    existVariant.d = input.d ;
                    existVariant.a = input.a;

                    _context.Variants.Update(existVariant);
                    _context.SaveChanges();
                }
                else
                {
                    //добавление варианта
                    var variant = new Variant
                    {
                        Name = input.Name,
                        H  = input.H,
                        t1 = input.t1,
                        T =  input.T,
                        w =  input.w,
                        C =  input.C,
                        Gm = input.Gm,
                        Cm = input.Cm,
                        d  = input.d,
                        a  = input.a,
                        UserId = _userId,
                        CreatedAt = DateTime.Now
                    };

                    _context.Variants.Add(variant);
                    _context.SaveChanges();
                }
            }
            //Выполнение расчёта
            var lib = new Teploobmen(input);
            var result = lib.calc();
            if (ModelState.IsValid)
                return View(result);
            TempData["message"] = $"Заполните все поля";
            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Index(int? variantId)
        {
            var viewModel = new HomeIndexViewModel();

            if (variantId != null)
            {
                viewModel.Variant = _context.Variants
                    .Where(x => x.UserId == _userId || x.UserId == 0)
                    .FirstOrDefault(x => x.Id == variantId);
            }

            viewModel.Variants = _context.Variants
                .Where(x => x.UserId == _userId || x.UserId == 0)
                .ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Remove(int? variantId)
        {
            var variant = _context.Variants
                .Where(x => x.UserId == _userId || x.UserId == 0)
                .FirstOrDefault(x => x.Id == variantId);

            if (variant != null)
            {
                _context.Variants.Remove(variant);
                _context.SaveChanges();

                TempData["message"] = $"Вариант {variant.Name} удален.";
            }
            else
            {
                TempData["message"] = $"Вариант не найден.";
            }

            return RedirectToAction(nameof(Index));
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