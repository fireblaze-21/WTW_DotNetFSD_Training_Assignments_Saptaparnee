use UniverityDB
CREATE VIEW vw_StudentCourseDepartmentInfo AS
SELECT 
    s.StudentID,
    s.FirstName + ' ' + s.LastName AS FullName,
    c.CourseID,
    c.CourseName,
    d.DepartmentName
FROM Students s
JOIN Enrollments e ON s.StudentID = e.StudentID
JOIN Courses c ON e.CourseID = c.CourseID
LEFT JOIN Departments d ON s.DepartmentID = d.DepartmentID;


SELECT * FROM vw_StudentCourseDepartmentInfo

-------------------------------------------------------------------------

CREATE NONCLUSTERED INDEX idx_Enrollments_StudentID 
ON Enrollments (StudentID)
INCLUDE (CourseID, EnrollmentDate);

CREATE NONCLUSTERED INDEX idx_Enrollments_CourseID 
ON Enrollments (CourseID)
INCLUDE (StudentID, EnrollmentDate);

SELECT * 
FROM sys.dm_db_index_usage_stats 
WHERE object_id = OBJECT_ID('Enrollments');

-------------------------------------------------------------------------------------


CREATE VIEW vw_DepartmentEnrollmentTrends AS
SELECT 
    d.DepartmentID,
    d.DepartmentName,
    YEAR(e.EnrollmentDate) AS AcademicYear,
    COUNT(DISTINCT e.StudentID) AS TotalEnrollments
FROM 
    Enrollments e
JOIN 
    Courses c ON e.CourseID = c.CourseID
JOIN 
    Departments d ON c.DepartmentID = d.DepartmentID
GROUP BY 
    d.DepartmentID, d.DepartmentName, YEAR(e.EnrollmentDate);

SELECT * FROM vw_DepartmentEnrollmentTrends
WHERE AcademicYear = 2024
ORDER BY TotalEnrollments DESC;

CREATE NONCLUSTERED INDEX idx_Enrollments_EnrollmentDate_StudentID
ON Enrollments (EnrollmentDate)
INCLUDE (StudentID, CourseID);

CREATE NONCLUSTERED INDEX idx_Courses_DepartmentID
ON Courses (CourseID)
INCLUDE (DepartmentID);

CREATE NONCLUSTERED INDEX idx_RecentEnrollments
ON Enrollments (EnrollmentDate)
WHERE EnrollmentDate >= '2020-01-01';


SELECT TOP 5 DepartmentName, TotalEnrollments
FROM vw_DepartmentEnrollmentTrends
WHERE AcademicYear = 2024
ORDER BY TotalEnrollments DESC;

SELECT AcademicYear, TotalEnrollments
FROM vw_DepartmentEnrollmentTrends
WHERE DepartmentName = 'Computer Science'
ORDER BY AcademicYear;


-----------------------------------------------------------------------


-- Classrooms Table
CREATE TABLE Classrooms (
    ClassroomID INT PRIMARY KEY,
    RoomNumber VARCHAR(10),
    Building VARCHAR(50)
);

-- Schedules Table
CREATE TABLE Schedules (
    ScheduleID INT PRIMARY KEY,
    CourseID INT,
    InstructorID INT,
    ClassroomID INT,
    ScheduleDate DATE,
    StartTime TIME,
    EndTime TIME,
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    FOREIGN KEY (InstructorID) REFERENCES Professors(ProfessorID),
    FOREIGN KEY (ClassroomID) REFERENCES Classrooms(ClassroomID)
);



-- Step 1: Ensure all base tables have primary keys (already done above)
-- Step 2: Create the view with SCHEMABINDING
CREATE VIEW dbo.vw_CourseScheduleInfo
WITH SCHEMABINDING
AS
SELECT 
    c.CourseID,
    c.CourseName,
    p.ProfessorID,
    p.FirstName + ' ' + p.LastName AS InstructorName,
    cl.ClassroomID,
    cl.RoomNumber,
    cl.Building,
    s.ScheduleDate,
    s.StartTime,
    s.EndTime
FROM 
    dbo.Courses c
JOIN 
    dbo.Schedules s ON c.CourseID = s.CourseID
JOIN 
    dbo.Professors p ON s.InstructorID = p.ProfessorID
JOIN 
    dbo.Classrooms cl ON s.ClassroomID = cl.ClassroomID;

-- Step 3: Create a unique clustered index to materialize the view
CREATE UNIQUE CLUSTERED INDEX idx_vw_CourseScheduleInfo
ON dbo.vw_CourseScheduleInfo (CourseID, ScheduleDate, StartTime);


-- ==============================================
-- Sample Queries for Faculty Access
-- ==============================================

-- a) Courses scheduled for a specific date
SELECT CourseName, InstructorName, Building, RoomNumber, StartTime, EndTime
FROM dbo.vw_CourseScheduleInfo
WHERE ScheduleDate = '2025-09-01'
ORDER BY StartTime;

-- b) Weekly schedule for a specific professor
SELECT ScheduleDate, CourseName, StartTime, EndTime, Building, RoomNumber
FROM dbo.vw_CourseScheduleInfo
WHERE ProfessorID = 1001
  AND ScheduleDate BETWEEN '2025-09-01' AND '2025-09-07'
ORDER BY ScheduleDate, StartTime;


---------------------------------------------------------------------------------


-- ==============================================
-- 1. Create a View for Student Grade Reports
-- ==============================================

-- Create a view to simplify access to student grade reports
CREATE VIEW dbo.vw_StudentGradeReport
AS
SELECT 
    s.StudentID,
    s.FirstName + ' ' + s.LastName AS StudentName,
    c.CourseID,
    c.CourseName,
    e.Grade,
    e.EnrollmentDate
FROM 
    dbo.Students s
JOIN 
    dbo.Enrollments e ON s.StudentID = e.StudentID
JOIN 
    dbo.Courses c ON e.CourseID = c.CourseID;

-- ==============================================
-- 2. Create Covering Indexes to Optimize the View
-- ==============================================

-- Index to optimize filtering by StudentID and quick retrieval of grade-related columns
CREATE NONCLUSTERED INDEX idx_Enrollments_StudentGradeReport
ON dbo.Enrollments (StudentID, CourseID)
INCLUDE (Grade, EnrollmentDate);

-- Index to optimize Course joins
CREATE NONCLUSTERED INDEX idx_Courses_CourseID_Name
ON dbo.Courses (CourseID)
INCLUDE (CourseName);

-- Index to optimize Student joins
CREATE NONCLUSTERED INDEX idx_Students_StudentID_Name
ON dbo.Students (StudentID)
INCLUDE (FirstName, LastName);


-- ==============================================
-- 3. Sample Usage Queries
-- ==============================================

-- a) Generate full grade report for a student
SELECT * 
FROM dbo.vw_StudentGradeReport
WHERE StudentID = 201;

-- b) Get all grades for a specific course
SELECT StudentName, Grade
FROM dbo.vw_StudentGradeReport
WHERE CourseName = 'Database Systems';

-- c) Get students who enrolled and got a grade 'A' in 2025
SELECT StudentName, CourseName, Grade
FROM dbo.vw_StudentGradeReport
WHERE Grade = 'A'
  AND YEAR(EnrollmentDate) = 2025;
