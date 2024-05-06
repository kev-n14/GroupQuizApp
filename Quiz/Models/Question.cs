using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Quiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Category { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public string QuestionText { get; set; }
        public int Points { get; set; }
        public List<Answer> Answers { get; set; }
    }
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
