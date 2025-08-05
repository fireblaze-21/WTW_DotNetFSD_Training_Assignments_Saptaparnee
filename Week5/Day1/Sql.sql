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
(2, 'Environmental Science'),   -- Contains 'Science' for Q2
(3, 'Physics'),
(4, 'Literature'),
(5, 'Economics');

-- Professors
INSERT INTO Professors (ProfessorID, FirstName, LastName, DepartmentID)
VALUES 
(101, 'Alice', 'Johnson', 1),       -- Head of CS (Q10)
(102, 'Robert', 'Brown', 2),        -- Head of Environmental Science
(103, 'Emily', 'Davis', 3),
(104, 'Mark', 'Lee', 4),
(105, 'Nina', 'Patel', 5),
(106, 'George', 'White', 1),
(107, 'Lily', 'Kim', 2),
(108, 'Jack', 'Turner', 5);         -- First name starts with J (Q10)

-- Set department heads
UPDATE Departments SET HeadProfessorID = 101 WHERE DepartmentID = 1;
UPDATE Departments SET HeadProfessorID = 102 WHERE DepartmentID = 2;
UPDATE Departments SET HeadProfessorID = 103 WHERE DepartmentID = 3;
UPDATE Departments SET HeadProfessorID = 104 WHERE DepartmentID = 4;
UPDATE Departments SET HeadProfessorID = 105 WHERE DepartmentID = 5;
UPDATE Departments SET HeadProfessorID = 108 WHERE DepartmentID = 5; -- For Q10

-- Students
INSERT INTO Students (StudentID, FirstName, LastName, DateOfBirth, DepartmentID)
VALUES 
(201, 'Alice', 'Green', '2002-05-10', 1),     -- Q1: name starts with 'A'
(202, 'Stu1', 'Test', '2000-01-01', 2),       -- Q2 student 1
(203, 'Stu2', 'Test', '2000-01-01', 2),
(204, 'Stu3', 'Test', '2000-01-01', 2),
(205, 'Stu4', 'Test', '2000-01-01', 2),
(206, 'Stu5', 'Test', '2000-01-01', 2),
(207, 'Stu6', 'Test', '2000-01-01', 2),       -- Total 6 students for Q2
(208, 'Brian', 'Wong', '2001-02-02', 4),      -- Q5: student with no F
(209, 'Grace', 'Lee', '2000-06-01', 5),       -- Q8: no enrollments
(210, 'Anna', 'Gray', '2001-08-08', 1),       -- Q9
(211, 'Tom', 'Black', '2001-09-09', 1);       -- Q9

-- Courses
INSERT INTO Courses (CourseID, CourseName, DepartmentID, Credits)
VALUES 
(301, 'Database Systems', 1, 4),         -- Q1
(302, 'Environmental Models', 2, 3),     -- For Q2 (linking course to dept)
(303, 'Poetry', 4, 3),                   -- Q5 course
(304, 'Data Mining', 1, 4),              -- Q9 course with 'Data'
(305, 'Algebra', 2, 3),                  -- Q6
(306, 'Independent Study', NULL, 2);     -- Q3: DepartmentID is NULL

-- Enrollments
INSERT INTO Enrollments (EnrollmentID, StudentID, CourseID, Grade, EnrollmentDate)
VALUES 
(401, 201, 301, 'A', '2023-01-15'),         -- Alice enrolled (Q1)
(402, 208, 303, 'B', '2024-01-15'),         -- Brian has no 'F' (Q5)
(403, 210, 304, 'A', '2024-05-01'),         -- Q9: Anna gets A in Data course
(404, 211, 304, 'B', '2024-05-01'),         -- Q9: Tom gets B in Data course
(405, 208, 305, 'C', '2024-04-10');         -- Q7: Grade NOT NULL in 2024




--1. Find students whose first names start with 'A' 
--and who are enrolled in at least one course, using LIKE and IS NOT NULL.

SELECT DISTINCT s.StudentID, s.FirstName, s.LastName
FROM Students s
JOIN Enrollments e ON s.StudentID = e.StudentID
WHERE s.FirstName LIKE 'A%' 
  AND e.CourseID IS NOT NULL;
  
--2. Departments where the department name contains 'Science' and more than 5 students belong

SELECT d.DepartmentID, d.DepartmentName, COUNT(s.StudentID) AS StudentCount
FROM Departments d
LEFT JOIN Students s ON d.DepartmentID = s.DepartmentID
WHERE d.DepartmentName LIKE '%Science%'
GROUP BY d.DepartmentID, d.DepartmentName
HAVING COUNT(s.StudentID) > 5;

--3. Courses with no assigned department

SELECT CourseID, CourseName, Credits
FROM Courses
WHERE DepartmentID IS NULL;

--4. Professors whose last names do NOT contain 'e' and who are associated with a department


SELECT ProfessorID, FirstName, LastName, DepartmentID
FROM Professors
WHERE LastName NOT LIKE '%e%'
  AND DepartmentID IS NOT NULL;
  
--Students who have never received a failing grade ('F')


SELECT s.StudentID, s.FirstName, s.LastName
FROM Students s
WHERE s.StudentID NOT IN (
    SELECT e.StudentID
    FROM Enrollments e
    WHERE e.Grade = 'F'
);


--6. Total number of credits offered by each department (only departments with at least one course)

SELECT d.DepartmentID, d.DepartmentName, SUM(c.Credits) AS TotalCredits
FROM Departments d
JOIN Courses c ON d.DepartmentID = c.DepartmentID
WHERE d.DepartmentID IS NOT NULL
GROUP BY d.DepartmentID, d.DepartmentName
HAVING COUNT(c.CourseID) > 0;

--7. Enrollments where the grade is not null and enrollment happened in the year 2024


SELECT EnrollmentID, StudentID, CourseID, Grade, EnrollmentDate
FROM Enrollments
WHERE Grade IS NOT NULL
  AND EnrollmentDate >= '2024-01-01' 
  AND EnrollmentDate < '2025-01-01';
  
--8. Students who are NOT enrolled in any courses and have a non-null birthdate


SELECT s.StudentID, s.FirstName, s.LastName, s.DateOfBirth
FROM Students s
LEFT JOIN Enrollments e ON s.StudentID = e.StudentID
WHERE e.EnrollmentID IS NULL
  AND s.DateOfBirth IS NOT NULL;
  
--9.	Write a query to identify courses where the course name contains 'Data'
--and the average grade of enrolled students is above 3.0, using LIKE, GROUP BY, and
--a CASE statement for grade conversion.

SELECT c.CourseID, c.CourseName, AVG(
    CASE e.Grade
        WHEN 'A' THEN 4.0
        WHEN 'B' THEN 3.0
        WHEN 'C' THEN 2.0
        WHEN 'D' THEN 1.0
        WHEN 'F' THEN 0.0
        ELSE NULL
    END
) AS AvgGrade
FROM Courses c
JOIN Enrollments e ON c.CourseID = e.CourseID
WHERE c.CourseName LIKE '%Data%'
GROUP BY c.CourseID, c.CourseName
HAVING AVG(
    CASE e.Grade
        WHEN 'A' THEN 4.0
        WHEN 'B' THEN 3.0
        WHEN 'C' THEN 2.0
        WHEN 'D' THEN 1.0
        WHEN 'F' THEN 0.0
        ELSE NULL
    END
) > 3.0;


--10. Professors who are department heads and whose first names start with 'J' or 'M'
SELECT p.ProfessorID, p.FirstName, p.LastName
FROM Professors p
WHERE (p.FirstName LIKE 'J%' OR p.FirstName LIKE 'M%')
  AND p.ProfessorID IN (
    SELECT d.HeadProfessorID
    FROM Departments d
    WHERE d.HeadProfessorID IS NOT NULL
  );