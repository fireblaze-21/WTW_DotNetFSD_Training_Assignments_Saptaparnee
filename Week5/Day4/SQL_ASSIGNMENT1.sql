CREATE DATABASE UniversityDB;
GO

USE UniversityDB;
GO

CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL
);

CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseCode NVARCHAR(10) UNIQUE NOT NULL,
    CourseName NVARCHAR(100) NOT NULL,
    CreditHours INT CHECK (CreditHours BETWEEN 1 AND 6),
    MaxCapacity INT CHECK (MaxCapacity > 0),
    PrerequisiteCourseID INT NULL,
    CONSTRAINT FK_Courses_Prereq FOREIGN KEY (PrerequisiteCourseID) REFERENCES Courses(CourseID)
);

CREATE TABLE StudentCourses (
    StudentID INT,
    CourseID INT,
    EnrollmentDate DATE DEFAULT GETDATE(),
    Grade CHAR(2) NULL,
    Status NVARCHAR(20) CHECK (Status IN ('Enrolled', 'Completed', 'Withdrawn')),
    PRIMARY KEY (StudentID, CourseID),
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

INSERT INTO Students (FirstName, LastName)
VALUES 
('Alice', 'Johnson'),--ID 1
('Bob', 'Smith'), --ID 2
('Charlie', 'Brown');
INSERT INTO Students (FirstName, LastName)
VALUES 
('David', 'Lee'),    -- ID 4
('Emma', 'Watson');  -- ID 5

-- Insert a few courses
INSERT INTO Courses (CourseCode, CourseName, CreditHours, MaxCapacity, PrerequisiteCourseID)
VALUES 
('CS101', 'Introduction to Computer Science', 3, 50, NULL), --COURSE ID 1
('MATH101', 'Calculus I', 4, 40, NULL), -- COURSE ID 2
('CS102', 'Data Structures', 4, 40, 1);  -- Prerequisite: CS101 -- COURSE ID 3
INSERT INTO Courses (CourseCode, CourseName, CreditHours, MaxCapacity, PrerequisiteCourseID)
VALUES 
('ENG101', 'English Literature', 3, 40, NULL), -- COURSE ID 4
('PHY101', 'Physics I', 4, 40, NULL), -- COURSE ID 5
('HIST201', 'World History', 3, 40, NULL);  -- COURSE ID 6

INSERT INTO StudentCourses (StudentID, CourseID, Grade, Status)
VALUES 
(1, 1, NULL, 'Enrolled'),   -- Alice in CS101
(2, 2, 'B+', 'Completed'),  -- Bob in MATH101
(3, 1, 'A', 'Completed'),   -- Charlie in CS101
(3, 3, NULL, 'Enrolled');   -- Charlie in CS102 (meets prerequisite)
-- David (ID 4): Completed 2 courses
INSERT INTO StudentCourses (StudentID, CourseID, Grade, Status)
VALUES 
(4, 4, 'A', 'Completed'),  -- ENG101
(4, 5, 'B', 'Completed');  -- PHY101

-- Emma (ID 5): Completed 1 course and one still enrolled
INSERT INTO StudentCourses (StudentID, CourseID, Grade, Status)
VALUES 
(5, 4, 'A', 'Completed'),     -- ENG101
(5, 6, NULL, 'Enrolled');     -- HIST201 — still enrolled, no grade


CREATE FUNCTION dbo.CalculateStudentGPA (@StudentID INT)
RETURNS DECIMAL(4, 2)
AS
BEGIN
    DECLARE @TotalGradePoints FLOAT = 0.0;
    DECLARE @TotalCreditHours INT = 0;
    DECLARE @GPA DECIMAL(4,2) = 0.00;

    SELECT 
        @TotalGradePoints = SUM(
            CASE sc.Grade
                WHEN 'A' THEN 4.0
                WHEN 'B' THEN 3.0
                WHEN 'C' THEN 2.0
                WHEN 'D' THEN 1.0
                WHEN 'F' THEN 0.0
                ELSE NULL
            END * c.CreditHours
        ),
        @TotalCreditHours = SUM(
            CASE 
                WHEN sc.Grade IS NOT NULL THEN c.CreditHours
                ELSE 0
            END
        )
    FROM StudentCourses sc
    JOIN Courses c ON sc.CourseID = c.CourseID
    WHERE sc.StudentID = @StudentID
      AND sc.Status = 'Completed'
      AND sc.Grade IS NOT NULL;

    IF @TotalCreditHours > 0
        SET @GPA = ROUND(@TotalGradePoints / @TotalCreditHours, 2);

    RETURN @GPA;
END;



SELECT dbo.CalculateStudentGPA(4) as GPA_David;


-----------------------------------------------------------------------------------