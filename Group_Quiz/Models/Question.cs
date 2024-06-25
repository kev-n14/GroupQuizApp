using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Group_Quiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        [Required]
        public QuestionCategory Category { get; set; } //PK for question entity

        [Required]
        public DifficultyLevel Difficulty { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string QuestionText { get; set; }

        [Range(0, 10, ErrorMessage = "Points must be between 0 and 10")]
        public int Points { get; set; }

        [Required]
        public string Locale { get; set; } // Captures the question language

        [Required]
        public QuestionType Type { get; set; } 

       
        public List<Answer> Answers { get; set; }// List of answers associated with question
        
        
        public bool HasCorrectAnswer => Answers?.Any(a => a.IsCorrect) ?? false; // Property to check if question has a correct answer

        [ConcurrencyCheck]
        public DateTime RowVersion { get; set; } = DateTime.UtcNow;// prob for concuurency control
    }


    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    public enum QuestionCategory
    {
        Science,
        History,
        Geography,
        Movies,


    }

    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,

    }

    public class Answer
    {
        public int AnswerId { get; set; }//PK for Answer entity

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
       
        

    }
    public class ResultViewModel
    {
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalPossibleScore { get; set; }
    }

}
