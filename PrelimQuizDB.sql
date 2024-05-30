CREATE DATABASE IF NOT EXISTS quiz;
USE quiz;

##
CREATE TABLE questions (
    question_id INT PRIMARY KEY AUTO_INCREMENT,
    question_text TEXT NOT NULL,
    question_type ENUM('single', 'multiple', 'true_false') NOT NULL,
    category VARCHAR(100) NOT NULL, 			#Maybe have this enum once categories are decided
    difficulty ENUM('easy', 'medium', 'hard') NOT NULL,
    locale VARCHAR(50) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

###
CREATE TABLE answers (
    answer_id INT PRIMARY KEY AUTO_INCREMENT,
    question_id INT NOT NULL,
    answer_text VARCHAR(100) NOT NULL,
    is_correct BOOLEAN DEFAULT TRUE NOT NULL,
    FOREIGN KEY (question_id) REFERENCES questions(question_id) ON DELETE CASCADE
);

###
CREATE TABLE keywords (
    keyword_id INT PRIMARY KEY AUTO_INCREMENT,
    question_id INT NOT NULL,
    keyword VARCHAR(100) NOT NULL,
    FOREIGN KEY (question_id) REFERENCES questions(question_id) ON DELETE CASCADE
);

-- DROP TABLE questions;
-- DROP TABLE answers;
-- DROP TABLE keywords;