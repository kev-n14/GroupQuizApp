-- 
INSERT INTO questions (question_text, question_type, category, difficulty, locale)
VALUES ('What is the capital of France?', 'multiple', 'Geography', 'Easy', 'English');

-- Get the question_id to use as foreign key in answers and keywords table
SET @question_id = (SELECT question_id FROM questions WHERE question_text = 'What is the capital of France?' LIMIT 1);

-- Adding correct answer
INSERT INTO Answers (question_id, answer_text, is_correct)
VALUES (@question_id, 'Paris', TRUE);

-- Adding incorrect answers
INSERT INTO Answers (question_id, answer_text, is_correct)
VALUES (@question_id, 'London', FALSE),
       (@question_id, 'Berlin', FALSE),
       (@question_id, 'Madrid', FALSE);
       
-- Adding keywords for the question
INSERT INTO Keywords (question_id, keyword)
VALUES (@question_id, 'capital'),
       (@question_id, 'France'),
       (@question_id, 'Geography');