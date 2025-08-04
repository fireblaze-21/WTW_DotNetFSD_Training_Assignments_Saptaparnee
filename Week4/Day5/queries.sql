
/*Departments
o DepartmentID (Primary Key): Unique identifier for each department.
o DepartmentName: Name of the department (e.g., Computer Science, Mathematics).
o HeadProfessorID (Foreign Key): References the professor who heads the 
department*/
-- Departments Table
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100) NOT NULL,
    HeadProfessorID INT -- Will be set later after Professors table is created
);

/*1. Students
o StudentID (Primary Key): Unique identifier for each student.
o FirstName: Student's first name.
o LastName: Student's last name.
o DateOfBirth: Student's date of birth.
o DepartmentID (Foreign Key): References the department the student belongs to*/
-- Students Table
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DateOfBirth DATE,
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);


/*Professors
o ProfessorID (Primary Key): Unique identifier for each professor.
o FirstName: Professor's first name.
o LastName: Professor's last name.
o DepartmentID (Foreign Key): References the department the professor is associated 
with.*/
-- Professors Table
CREATE TABLE Professors (
    ProfessorID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

-- Update Departments with foreign key after Professors exists
ALTER TABLE Departments
ADD CONSTRAINT FK_Department_Head FOREIGN KEY (HeadProfessorID)
REFERENCES Professors(ProfessorID);

/*Courses
o CourseID (Primary Key): Unique identifier for each course.
o CourseName: Name of the course (e.g., Database Systems, Calculus).
o DepartmentID (Foreign Key): References the department offering the course.
o Credits: Number of credits for the course.*/

-- Courses Table
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY,
    CourseName VARCHAR(100),
    DepartmentID INT,
    Credits INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

/*Enrollments
o EnrollmentID (Primary Key): Unique identifier for each enrollment record.
o StudentID (Foreign Key): References the student enrolled in the course.
SQL Server Case Study
Trainer: Narasimha Rao T
o CourseID (Foreign Key): References the course the student is enrolled in.
o Grade: Student's grade in the course (e.g., A, B, F).
o EnrollmentDate: Date when the student enrolled in the cours*/
-- Enrollments Table
CREATE TABLE Enrollments (
    EnrollmentID INT PRIMARY KEY,
    StudentID INT,
    CourseID INT,
    Grade CHAR(2),
    EnrollmentDate DATE,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);



-- ======================
-- INSERT SAMPLE DATA
-- ======================

-- Departments
INSERT INTO Departments (DepartmentID, DepartmentName)
VALUES 
(1, 'Computer Science'),
(2, 'Mathematics'),
(3, 'Physics'),
(4, 'Literature'),
(5, 'Economics');

-- Professors
INSERT INTO Professors (ProfessorID, FirstName, LastName, DepartmentID)
VALUES 
(101, 'Alice', 'Johnson', 1),
(102, 'Robert', 'Brown', 2),
(103, 'Emily', 'Davis', 3),
(104, 'Mark', 'Lee', 4),
(105, 'Nina', 'Patel', 5),
(106, 'George', 'White', 1),
(107, 'Lily', 'Kim', 2);

-- Set department heads
UPDATE Departments SET HeadProfessorID = 101 WHERE DepartmentID = 1;
UPDATE Departments SET HeadProfessorID = 102 WHERE DepartmentID = 2;
UPDATE Departments SET HeadProfessorID = 103 WHERE DepartmentID = 3;
UPDATE Departments SET HeadProfessorID = 104 WHERE DepartmentID = 4;
UPDATE Departments SET HeadProfessorID = 105 WHERE DepartmentID = 5;

-- Students
INSERT INTO Students (StudentID, FirstName, LastName, DateOfBirth, DepartmentID)
VALUES 
(201, 'John', 'Smith', '2001-05-10', 1),
(202, 'Sarah', 'Williams', '2002-08-21', 1),
(203, 'Michael', 'Johnson', '2000-03-15', 2),
(204, 'Emma', 'Wilson', '2001-11-11', 3),
(205, 'David', 'Lee', '1999-12-20', 4),
(206, 'Olivia', 'Martin', '2003-07-08', 5),
(207, 'James', 'Taylor', '2002-10-01', 2),
(208, 'Sophia', 'Anderson', '2001-03-19', 1),
(209, 'Daniel', 'Thomas', '2000-06-25', 3),
(210, 'Chloe', 'Clark', '2002-09-05', 5);

-- Courses
INSERT INTO Courses (CourseID, CourseName, DepartmentID, Credits)
VALUES 
(301, 'Database Systems', 1, 4),
(302, 'Algorithms', 1, 3),
(303, 'Calculus', 2, 4),
(304, 'Linear Algebra', 2, 3),
(305, 'Quantum Mechanics', 3, 5),
(306, 'Modern Poetry', 4, 2),
(307, 'Microeconomics', 5, 3),
(308, 'Operating Systems', 1, 4),
(309, 'Classical Literature', 4, 3),
(310, 'Macroeconomics', 5, 3);

-- Enrollments
INSERT INTO Enrollments (EnrollmentID, StudentID, CourseID, Grade, EnrollmentDate)
VALUES 
(401, 201, 301, 'A', '2023-01-15'),
(402, 201, 302, 'B', '2023-01-17'),
(403, 202, 301, 'A', '2023-01-20'),
(404, 203, 303, 'C', '2023-01-22'),
(405, 204, 305, 'B', '2023-02-01'),
(406, 204, 304, 'A', '2023-02-05'),
(407, 205, 306, 'A', '2023-02-10'),
(408, 205, 309, 'B', '2023-03-01'),
(409, 206, 307, 'A', '2023-03-05'),
(410, 206, 310, 'B', '2023-03-10'),
(411, 207, 303, 'B', '2023-03-12'),
(412, 208, 308, 'A', '2023-03-15'),
(413, 208, 301, 'B', '2023-03-18'),
(414, 209, 305, 'C', '2023-03-20'),
(415, 210, 307, 'A', '2023-03-25'),
(416, 210, 310, 'A', '2023-03-28');

-- 1. Retrieve all records from the Students table.
SELECT s.StudentID, s.FirstName, s.LastName, s.DateOfBirth, s.DepartmentID
FROM Students AS s;
-------------------------

-- 2. List the first and last names of students who belong to DepartmentID 1.
SELECT s.FirstName, s.LastName
FROM Students AS s
WHERE s.DepartmentID = 1;
-------------------------

-- 3. Display course names and their corresponding department names using an INNER JOIN.
SELECT c.CourseName, d.DepartmentName
FROM Courses AS c
INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID;
-------------------------

-- 4. Count the number of students in each department, including departments with zero students.
SELECT d.DepartmentName, COUNT(s.StudentID) AS StudentCount
FROM Departments AS d
LEFT JOIN Students AS s ON d.DepartmentID = s.DepartmentID
GROUP BY d.DepartmentName;
-------------------------

-- 5. List all students’ names, sorted by last name in ascending order.
SELECT s.FirstName, s.LastName
FROM Students AS s
ORDER BY s.LastName ASC;
-------------------------

-- 6. Find students who received an 'A' grade in any course.
SELECT s.FirstName, s.LastName, e.Grade
FROM Enrollments AS e
INNER JOIN Students AS s ON e.StudentID = s.StudentID
WHERE e.Grade = 'A';
-------------------------

-- 7. Retrieve the names of courses that have more than 3 credits.
SELECT c.CourseName
FROM Courses AS c
WHERE c.Credits > 3;
-------------------------

-- 8. List professors’ names and their department names, including unassigned professors.
SELECT p.FirstName, p.LastName, d.DepartmentName
FROM Professors AS p
LEFT JOIN Departments AS d ON p.DepartmentID = d.DepartmentID;
-------------------------

-- 9. Find names of students enrolled in CourseID 101 (using subquery).
SELECT s.FirstName, s.LastName
FROM Students AS s
WHERE s.StudentID IN (
    SELECT e.StudentID
    FROM Enrollments AS e
    WHERE e.CourseID = 101
);
-------------------------

-- 10. List students and total credits earned using a CTE.
WITH CreditCTE AS (
    SELECT e.StudentID, SUM(c.Credits) AS TotalCredits
    FROM Enrollments AS e
    INNER JOIN Courses AS c ON e.CourseID = c.CourseID
    GROUP BY e.StudentID
)
SELECT s.FirstName, s.LastName, cte.TotalCredits
FROM Students AS s
INNER JOIN CreditCTE AS cte ON s.StudentID = cte.StudentID;
-------------------------

-- 11. Identify departments with no students enrolled.
SELECT d.DepartmentName
FROM Departments AS d
WHERE d.DepartmentID NOT IN (
    SELECT s.DepartmentID
    FROM Students AS s
);
-------------------------

-- 12. Top 5 students by average grade (assuming letter to numeric conversion).
WITH GradeCTE AS (
    SELECT 
        e.StudentID,
        AVG(CASE e.Grade 
                WHEN 'A' THEN 4 
                WHEN 'B' THEN 3 
                WHEN 'C' THEN 2 
                WHEN 'D' THEN 1 
                WHEN 'F' THEN 0 
            END) AS AvgGrade
    FROM Enrollments AS e
    GROUP BY e.StudentID
)
SELECT TOP 5 s.FirstName, s.LastName, gcte.AvgGrade
FROM GradeCTE AS gcte
INNER JOIN Students AS s ON s.StudentID = gcte.StudentID
ORDER BY gcte.AvgGrade DESC;
-------------------------

-- 13. Find courses that have no enrollments.
SELECT c.CourseName
FROM Courses AS c
WHERE c.CourseID NOT IN (
    SELECT e.CourseID
    FROM Enrollments AS e
);
-------------------------

-- 14. Retrieve professors who are department heads (using subquery).
SELECT p.FirstName, p.LastName
FROM Professors AS p
WHERE p.ProfessorID IN (
    SELECT d.HeadProfessorID
    FROM Departments AS d
);
-------------------------

-- 15. List students with enrollments after January 1, 2023.
SELECT s.FirstName, s.LastName, e.EnrollmentDate
FROM Enrollments AS e
INNER JOIN Students AS s ON e.StudentID = s.StudentID
WHERE e.EnrollmentDate > '2023-01-01';
-------------------------

-- 16. Average credits per course by department using CTE.
WITH DeptAvgCTE AS (
    SELECT d.DepartmentName, AVG(c.Credits) AS AvgCredits
    FROM Departments AS d
    INNER JOIN Courses AS c ON c.DepartmentID = d.DepartmentID
    GROUP BY d.DepartmentName
)
SELECT dcte.DepartmentName, dcte.AvgCredits
FROM DeptAvgCTE AS dcte
ORDER BY dcte.AvgCredits DESC;
-------------------------

-- 17. Find students enrolled in more than one course.
SELECT s.FirstName, s.LastName, COUNT(e.CourseID) AS CourseCount
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentID = e.StudentID
GROUP BY s.StudentID, s.FirstName, s.LastName
HAVING COUNT(e.CourseID) > 1;
-------------------------

-- 18. List course names with head professor names.
SELECT c.CourseName, p.FirstName, p.LastName
FROM Courses AS c
INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID
INNER JOIN Professors AS p ON d.HeadProfessorID = p.ProfessorID;
-------------------------

-- 19. Students with grades above the overall average (subquery + CTE).
WITH OverallAvgCTE AS (
    SELECT AVG(CASE Grade 
                WHEN 'A' THEN 4 
                WHEN 'B' THEN 3 
                WHEN 'C' THEN 2 
                WHEN 'D' THEN 1 
                WHEN 'F' THEN 0 
            END) AS OverallAverage
    FROM Enrollments
), StudentAvgCTE AS (
    SELECT e.StudentID, AVG(CASE e.Grade 
                            WHEN 'A' THEN 4 
                            WHEN 'B' THEN 3 
                            WHEN 'C' THEN 2 
                            WHEN 'D' THEN 1 
                            WHEN 'F' THEN 0 
                        END) AS StudentAverage
    FROM Enrollments AS e
    GROUP BY e.StudentID
)
SELECT s.FirstName, s.LastName
FROM StudentAvgCTE AS scte
JOIN OverallAvgCTE AS oavg ON scte.StudentAverage > oavg.OverallAverage
JOIN Students AS s ON s.StudentID = scte.StudentID;
-------------------------

-- 20. Departments with more than 10 students.
SELECT d.DepartmentName, COUNT(s.StudentID) AS StudentCount
FROM Departments AS d
INNER JOIN Students AS s ON d.DepartmentID = s.DepartmentID
GROUP BY d.DepartmentName
HAVING COUNT(s.StudentID) > 10;
-------------------------

-- 21. Students not enrolled in any courses.
SELECT s.FirstName, s.LastName
FROM Students AS s
WHERE s.StudentID NOT IN (
    SELECT e.StudentID
    FROM Enrollments AS e
);
-------------------------

-- 22. Number of enrollments per course using CTE.
WITH EnrollCountCTE AS (
    SELECT e.CourseID, COUNT(*) AS EnrollmentCount
    FROM Enrollments AS e
    GROUP BY e.CourseID
)
SELECT c.CourseName, ec.EnrollmentCount
FROM EnrollCountCTE AS ec
JOIN Courses AS c ON ec.CourseID = c.CourseID
ORDER BY ec.EnrollmentCount DESC;
-------------------------

-- 23. Students born after 2000.
SELECT s.FirstName, s.LastName, s.DateOfBirth
FROM Students AS s
WHERE s.DateOfBirth > '2000-12-31';
-------------------------

-- 24. Courses and total students enrolled, including zero enrollments.
SELECT c.CourseName, COUNT(e.EnrollmentID) AS TotalEnrollments
FROM Courses AS c
LEFT JOIN Enrollments AS e ON c.CourseID = e.CourseID
GROUP BY c.CourseName;
-------------------------

-- 25. Courses offered by Computer Science department (subquery).
SELECT c.CourseName
FROM Courses AS c
WHERE c.DepartmentID = (
    SELECT d.DepartmentID
    FROM Departments AS d
    WHERE d.DepartmentName = 'Computer Science'
);
-------------------------

-- 26. Professors not heading any department.
SELECT p.FirstName, p.LastName
FROM Professors AS p
WHERE p.ProfessorID NOT IN (
    SELECT d.HeadProfessorID
    FROM Departments AS d
);
-------------------------

-- 27. Students, department, and course count (include zero enrollments).
SELECT s.FirstName, s.LastName, d.DepartmentName, COUNT(e.EnrollmentID) AS EnrollCount
FROM Students AS s
INNER JOIN Departments AS d ON s.DepartmentID = d.DepartmentID
LEFT JOIN Enrollments AS e ON s.StudentID = e.StudentID
GROUP BY s.StudentID, s.FirstName, s.LastName, d.DepartmentName;
-------------------------

-- 28. Students who received an 'F' grade using CTE.
WITH FailCTE AS (
    SELECT DISTINCT e.StudentID, e.Grade
    FROM Enrollments AS e
    WHERE e.Grade = 'F'
)
SELECT s.FirstName, s.LastName, fcte.Grade
FROM FailCTE AS fcte
JOIN Students AS s ON fcte.StudentID = s.StudentID;
-------------------------

-- 29. Courses offered by multiple departments (if CourseName reused).
SELECT c.CourseName, COUNT(DISTINCT c.DepartmentID) AS DepartmentCount
FROM Courses AS c
GROUP BY c.CourseName
HAVING COUNT(DISTINCT c.DepartmentID) > 1;
-------------------------

-- 30. Students and their most recent enrollment date.
SELECT s.FirstName, s.LastName, MAX(e.EnrollmentDate) AS MostRecentEnrollment
FROM Students AS s
JOIN Enrollments AS e ON s.StudentID = e.StudentID
GROUP BY s.StudentID, s.FirstName, s.LastName;
-------------------------