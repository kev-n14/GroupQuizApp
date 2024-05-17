using Group_Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Group_Quiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            // Logic to create a new quiz
            return View();
        }
        public IActionResult Dashboard()
        {
            // Logic to display quiz questions
            return View();
        }
        public IActionResult Edit()
        {
            // Logic to display quiz questions
            return View();
        }
        public IActionResult Quiz()
        {
            // Logic to display quiz questions
            return View();
        }
        public IActionResult Result()
        {
            // Logic to display quiz questions
            return View();
        }

        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
