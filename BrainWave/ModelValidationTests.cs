using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Group_Quiz.Models;

namespace Group_Quiz.Tests
{
    public class ModelValidationTests
    {
        [Fact]
        public void Question_ValidModel_PassesValidation()
        {
            // Arrange
            var question = new Question
            {
                QuestionId = 1,
                Category = QuestionCategory.Science,
                Difficulty = DifficultyLevel.Medium,
                QuestionText = "Valid question text",
                Points = 5,
                Locale = "en-US",
                Type = QuestionType.MultipleChoice,
                Answers = new List<Answer>
                {
                    new Answer { AnswerId = 1, Text = "Answer 1", IsCorrect = true },
                    new Answer { AnswerId = 2, Text = "Answer 2", IsCorrect = false }
                }
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(question, new ValidationContext(question), validationResults, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Question_InvalidModel_FailsValidation()
        {
            // Arrange
            var question = new Question
            {
                QuestionId = 1,
                Category = QuestionCategory.Science,
                Difficulty = DifficultyLevel.Medium,
                QuestionText = "A", // Too short
                Points = 15, // Out of range
                Locale = "", // Empty
                Type = QuestionType.MultipleChoice,
                Answers = new List<Answer>()
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(question, new ValidationContext(question), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Equal(4, validationResults.Count); // 4 validation errors
        }

        [Fact]
        public void Question_HasCorrectAnswer_ReturnsTrue()
        {
            // Arrange
            var question = new Question
            {
                Answers = new List<Answer>
                {
                    new Answer { IsCorrect = false },
                    new Answer { IsCorrect = true },
                    new Answer { IsCorrect = false }
                }
            };

            // Act & Assert
            Assert.True(question.HasCorrectAnswer);
        }

        [Fact]
        public void Question_HasNoCorrectAnswer_ReturnsFalse()
        {
            // Arrange
            var question = new Question
            {
                Answers = new List<Answer>
                {
                    new Answer { IsCorrect = false },
                    new Answer { IsCorrect = false },
                    new Answer { IsCorrect = false }
                }
            };

            // Act & Assert
            Assert.False(question.HasCorrectAnswer);
        }

        [Fact]
        public void Question_NoAnswers_HasCorrectAnswerReturnsFalse()
        {
            // Arrange
            var question = new Question
            {
                Answers = null
            };

            // Act & Assert
            Assert.False(question.HasCorrectAnswer);
        }

        [Fact]
        public void Answer_ValidModel_PassesValidation()
        {
            // Arrange
            var answer = new Answer
            {
                AnswerId = 1,
                Text = "Valid answer text",
                IsCorrect = true
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(answer, new ValidationContext(answer), validationResults, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Answer_InvalidModel_FailsValidation()
        {
            // Arrange
            var answer = new Answer
            {
                AnswerId = 1,
                Text = "", // Empty string
                IsCorrect = true
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(answer, new ValidationContext(answer), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Single(validationResults); // 1 validation error
        }

        [Fact]
        public void ResultViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var result = new ResultViewModel
            {
                Score = 5,
                TotalQuestions = 10,
                TotalPossibleScore = 20
            };

            // Act & Assert
            Assert.Equal(5, result.Score);
            Assert.Equal(10, result.TotalQuestions);
            Assert.Equal(20, result.TotalPossibleScore);
        }
    }
}