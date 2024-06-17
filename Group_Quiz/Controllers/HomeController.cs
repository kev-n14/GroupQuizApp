using Group_Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Group_Quiz.Data;
using Group_Quiz.Models;


namespace Group_Quiz.Controllers
{

    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly QuizDbContext _context;

        //temporary in-memory storage for quizzes
        public static List<Question> _question = new List<Question> ();

        public HomeController(ILogger<HomeController> logger, QuizDbContext context)
        {
            _logger = logger;
            _context = context;
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
                _context.Questions.Add(question);
                _context.SaveChanges();
               
                return RedirectToAction("Dashboard");
            }
            return View(question);
        }

        public IActionResult Dashboard()
        {
            var questions = _context.Questions.Include(q => q.Answers).ToList();
            return View(questions);
        }
        public IActionResult Edit()
        {
            
            return View();
        }
        public IActionResult Quiz()
        {
            var questions = _context.Questions.Include(q => q.Answers).ToList();
            return View(questions);
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
