using Group_Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Group_Quiz.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Drawing;


namespace Group_Quiz.Controllers
{

    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly QuizDbContext _context;
        private const string ScoreKey = "QuizScore";// key for storing quiz scores in session.


        public HomeController(ILogger<HomeController> logger, QuizDbContext context)
        {
            _logger = logger;//Initializing logger
            _context = context;//Initializing fdatabase context allows for db operations
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
                Answers = new List<Answer> { new Answer()}// Init new question model with one empty answer
            };
            
            return View(model);//Return Creat view with the model
        }

        //Post Create Action
        [HttpPost]
        public IActionResult Create(Question question)
        {
            if (ModelState.IsValid)// checked if form data submitted is valid
            {
                if (question.Answers == null)
                {
                    question.Answers = new List<Answer>();//init Answers list, if its null
                }
                else
                {
                    question.Answers.RemoveAll(a => string.IsNullOrWhiteSpace(a.Text));// removs empty answers
                }

                // Ensure only one answer is marked as correct
                if (Request.Form.ContainsKey("CorrectAnswer"))
                {
                    int correctAnswerIndex = int.Parse(Request.Form["CorrectAnswer"]);
                    for (int i = 0; i < question.Answers.Count; i++)
                    {
                        question.Answers[i].IsCorrect = (i == correctAnswerIndex);
                    }
                }
                

                _context.Questions.Add(question);//Add question to DB
                _context.SaveChanges();//Saves changes to DB

                return RedirectToAction("Dashboard");//redirects to Dashboard action
            }
            return View(question); // returns Create view with model if the form dat is valid
        }

        //Dashboard Action
        public IActionResult Dashboard()
        {                                              //eager loading
            var questions = _context.Questions.Include(q => q.Answers).ToList();// Retrieves questions with their answers frm the DB
            return View(questions);//return dashboard view with list
        }

        //Edit Action
        public IActionResult Edit(int id)
        {
            var question = _context.Questions.Include(q => q.Answers).FirstOrDefault(q => q.QuestionId == id);
            if (question == null)
            {
                return NotFound(); // return 404 error if question not found
            }
            return View(question); //return view with question model
        }

        [HttpPost]
        public IActionResult SaveEdit(Question question)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingQuestion = _context.Questions
                        .Include(q => q.Answers)
                        .FirstOrDefault(q => q.QuestionId == question.QuestionId);

                    if (existingQuestion == null)
                    {
                        return NotFound();
                    }

                    _context.Entry(existingQuestion).CurrentValues.SetValues(question);//updats question properties

                    // Update answers
                    foreach (var answer in question.Answers)
                    {
                        var existingAnswer = existingQuestion.Answers
                            .FirstOrDefault(a => a.AnswerId == answer.AnswerId);

                        if (existingAnswer != null)
                        {
                            _context.Entry(existingAnswer).CurrentValues.SetValues(answer);// updates existing answer
                        }
                        else
                        {
                            existingQuestion.Answers.Add(answer);//add new answer
                        }
                    }

                    existingQuestion.Answers.RemoveAll(a => !question.Answers.Any(na => na.AnswerId == a.AnswerId)); // Removes answers that are no longer in the updated question.

                    // Ensure only one answer is marked as correct
                    int correctAnswerId = int.Parse(Request.Form["CorrectAnswer"]);
                    foreach (var answer in existingQuestion.Answers)
                    {
                        answer.IsCorrect = answer.AnswerId == correctAnswerId;
                    }

                    _context.SaveChanges();//save
                    return RedirectToAction("Dashboard");//redirect dashboard action
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;// rethrows exception if concurrency issue
                    }
                }
            }
            return View(question);//return edit view with model if form data invalid
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id); //checkes if question exists by id
        }


        //Quiz Action
        public async Task<IActionResult> Quiz(int? id, int currentIndex =0)
        {
            var questions = await _context.Questions.Include(q => q.Answers).ToListAsync();
            if (questions.Count == 0)
            {
                return View("Error");//error view
            }
            Question question = null;
            if (id.HasValue)
            {
                question = questions.FirstOrDefault(q => q.QuestionId == id.Value);//gets question by id 
            }

            if (question == null && questions.Count > 0)
            {
                question = questions[currentIndex];// if no id provided get question by current index
            }

            if (question == null)
            {
                return NotFound();
            }

            ViewData["CurrentQuestionIndex"] = currentIndex;//Sets current question index in ViewData
            ViewData["Questions"] = questions;// Sets question list in ViewData
            return View(question);
        }

        //NextQuestion Action
        [HttpPost]
        public async Task<IActionResult> NextQuestion(int questionId, int currentIndex, int selectedAnswerId)
        {
            var question = await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.QuestionId == questionId);
            if (question == null)
            {
                return NotFound();
            }

            var selectedAnswer = question.Answers.FirstOrDefault(a => a.AnswerId == selectedAnswerId);
            if (selectedAnswer != null && selectedAnswer.IsCorrect)
            {
                // Increase score
                int currentScore = HttpContext.Session.GetInt32(ScoreKey) ?? 0;
                HttpContext.Session.SetInt32(ScoreKey, currentScore + question.Points);// Increases the score if the selected answer is correct.
            }

            var questions = await _context.Questions.ToListAsync();
            if (currentIndex < questions.Count - 1)
            {
                return RedirectToAction("Quiz", new { id = questions[currentIndex + 1].QuestionId, currentIndex = currentIndex + 1 });// Redirects to the next question.
            }
            else
            {
                return RedirectToAction("Result");// Redirects to result page if no more questions.
            }
        }


        //Result Action
        public IActionResult Result()
        {
            int totalScore = HttpContext.Session.GetInt32(ScoreKey) ?? 0; //retrire totalScore in seesion, if no score found default 0
            int totalQuestions = _context.Questions.Count();
            int totalPossibleScore = _context.Questions.Sum(q => q.Points);// adds up all points in db

            var viewModel = new ResultViewModel //new instance 
            {
                Score = totalScore,
                TotalQuestions = totalQuestions,
                TotalPossibleScore = totalPossibleScore
            };

            // Reset the score for the next quiz
            HttpContext.Session.Remove(ScoreKey);

            return View(viewModel);//viewModel used to display result info
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
                _context.Answers.RemoveRange(question.Answers);//removes answers associated with question 
                _context.Questions.Remove(question);//then remove question
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });// returns error view with error details
        }
    }
}
