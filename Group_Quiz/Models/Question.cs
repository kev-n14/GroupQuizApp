using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Group_Quiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        [Required]
        public QuestionCategory Category { get; set; } // Changed to an enum

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
        public QuestionType Type { get; set; } // Added for question type

       
        public List<Answer> Answers { get; set; }

        [ConcurrencyCheck]
        public DateTime RowVersion { get; set; } = DateTime.UtcNow;
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
        public int AnswerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        
    }

}
