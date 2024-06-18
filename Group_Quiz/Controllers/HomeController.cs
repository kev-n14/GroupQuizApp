using Group_Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Group_Quiz.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace Group_Quiz.Controllers
{

    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly QuizDbContext _context;

       
        public HomeController(ILogger<HomeController> logger, QuizDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        //Index Action
        public IActionResult Index()
        {
            return View("Index");
        }

        //Get Create Action
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

        //Post Create Action
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
        
        //Dashboard Action
        public IActionResult Dashboard()
        {
            var questions = _context.Questions.Include(q => q.Answers).ToList();
            return View(questions);
        }

        //Edit Action
        public IActionResult Edit(int id)
        {
            var question = _context.Questions.Include(q => q.Answers).FirstOrDefault(q => q.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        public IActionResult SaveEdit(Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(question).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(question);
        
        }

        //Quiz Action
        public async Task<IActionResult> Quiz(int? id, int currentIndex =0)
        {
            var questions = await _context.Questions.Include(q => q.Answers).ToListAsync();
            if (questions.Count == 0)
            {
                return View("Error");
            }
            Question question = null;
            if (id.HasValue)
            {
                question = questions.FirstOrDefault(q => q.QuestionId == id.Value);
            }

            if (question == null && questions.Count > 0)
            {
                question = questions[currentIndex];
            }

            if (question == null)
            {
                return NotFound();
            }

            ViewData["CurrentQuestionIndex"] = currentIndex;
            ViewData["Questions"] = questions;
            return View(question);
        }

        //NextQuestion Action
        public async Task<IActionResult> NextQuestion(int questionId, int currentIndex, int selectedAnswerId)
        {
            var questions = await _context.Questions.Include(q => q.Answers).ToListAsync();

            if (currentIndex < questions.Count - 1)
            {
                return RedirectToAction("Quiz", new { id = questions[currentIndex + 1].QuestionId, currentIndex = currentIndex + 1 });
            }
            else 
            {
                return RedirectToAction("Result");
            }
        }

        //Result Action
        public IActionResult Result()
        {
            //calculate and display result
            return View();
        }


        public IActionResult Delete(int id)
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
