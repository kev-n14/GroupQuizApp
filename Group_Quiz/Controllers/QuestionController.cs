using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group_Quiz.Data;
using Group_Quiz.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Group_Quiz.Controllers
{
    [ApiController]// api controller
    [Route("api/[controller]")]//set base route to api/question
    public class QuestionController : ControllerBase
    {
        private readonly QuizDbContext _context;

        public QuestionController(QuizDbContext context)//constructor
        {
            _context = context;
        }

        //To GET: api/questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()// retrieve all questons
        {
            return await _context.Questions.Include(q => q.Answers).ToListAsync();// Retrieves questions with their answers frm the DB
        }

        //GET api/questions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)// get question based on id: is route parameter
        {
            var question = await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.QuestionId == id);

            if (question == null)
            {
                return NotFound();//404
            }
            return question;// reurn question
        
        }

        // POST api/questions
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, question);
        }
    }
}