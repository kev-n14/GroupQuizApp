using Group_Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Group_Quiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //temporary in-memory storage for quizzes
        public static List<Question> _question = new List<Question> ();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        
        [HttpGet]
        public IActionResult Create()
        {
        //initialize the question model with an empty answers list
            var model = new Question
            {
                Answers = new List<Answer> { new Answer()}//add one empty answer to start with
            };
            
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
            //ensure the answers list is initialized
                if (question.Answers == null)
                {
                    question.Answers = new List<Answer>();
                }
                // Generate a unique ID for the new question
                question.QuestionId = _question.Any() ? _question.Max(q => q.QuestionId) + 1 : 1;
                // Add the new question to the list
                _question.Add(question);
                return RedirectToAction("Dashboard");
            }
            return View(question);
        }

        public IActionResult Dashboard()
        {
            if (_question == null)
            {
                _question = new List<Question>();
            }
            
            return View(_question);
        }
        public IActionResult Edit()
        {
            
            return View();
        }
        public IActionResult Quiz()
        {
            
            return View(_question);
        }
        public IActionResult Result()
        {
            
            return View();
        }

        public IActionResult Delete(int id)
        {
            var question = _question.Find(q => q.QuestionId == id);
            if (question != null) {

                return NotFound();
            }
            return View(question);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
