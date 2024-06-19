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
                try
                {
                    // Fetch the existing question from the database
                    var existingQuestion = _context.Questions.Include(q => q.Answers)
                                                             .FirstOrDefault(q => q.QuestionId == question.QuestionId);

                    if (existingQuestion == null)
                    {
                        return NotFound();
                    }

                    // Update the existing question with the new values
                    existingQuestion.QuestionText = question.QuestionText;
                    existingQuestion.Category = question.Category;
                    existingQuestion.Difficulty = question.Difficulty;
                    existingQuestion.Points = question.Points;
                    existingQuestion.Locale = question.Locale;
                    existingQuestion.Type = question.Type;

                    // Remove existing answers and add updated answers
                    _context.Answers.RemoveRange(existingQuestion.Answers);
                    existingQuestion.Answers = question.Answers;

                    // Handle concurrency
                    _context.Entry(existingQuestion).Property(q => q.RowVersion).OriginalValue = question.RowVersion;

                    _context.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var databaseValues = entry.GetDatabaseValues();
                    if (databaseValues == null)
                    {
                        ModelState.AddModelError("", "Unable to save changes. The question was deleted by another user.");
                    }
                    else
                    {
                        var databaseEntity = (Question)databaseValues.ToObject();
                        ModelState.AddModelError("", "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        question.RowVersion = databaseEntity.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
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

            var question = _context.Questions.Include(q => q.Answers).FirstOrDefault(q => q.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            try
            {
                _context.Answers.RemoveRange(question.Answers);
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                
                var innerException = ex.InnerException;
                Console.WriteLine(innerException?.Message);
                throw; 
            }

            return RedirectToAction("Dashboard");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
