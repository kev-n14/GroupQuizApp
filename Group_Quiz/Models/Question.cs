namespace Group_Quiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public QuestionCategory Category { get; set; } // Changed to an enum
        public DifficultyLevel Difficulty { get; set; }
        public string QuestionText { get; set; }
        public int Points { get; set; }
        public string Locale { get; set; } // Captures the question language
        public QuestionType Type { get; set; } // Added for question type
        public List<Answer> Answers { get; set; }
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
        SingleAnswer,
        MultipleChoice,
        TrueFalse,

    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
