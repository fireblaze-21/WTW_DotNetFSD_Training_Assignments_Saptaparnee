CREATE PROCEDURE GenerateStudentTranscript
    @StudentID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Check if student exists
        IF NOT EXISTS (SELECT 1 FROM Students WHERE StudentID = @StudentID)
        BEGIN
            RAISERROR('Student not found.', 16, 1);
            RETURN;
        END

        -- Display Student Info
        PRINT '--- Student Transcript ---';

        SELECT 
            s.StudentID,
            FullName = CONCAT(s.FirstName, ' ', s.LastName),
            GPA = dbo.CalculateStudentGPA(@StudentID)
        FROM Students s
        WHERE s.StudentID = @StudentID;

        -- Display Course History
        SELECT 
            c.CourseCode,
            c.CourseName,
            c.CreditHours,
            sc.Grade,
            sc.EnrollmentDate,
            sc.Status
        FROM StudentCourses sc
        JOIN Courses c ON sc.CourseID = c.CourseID
        WHERE sc.StudentID = @StudentID
        ORDER BY sc.EnrollmentDate;

    END TRY

    BEGIN CATCH
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrState INT = ERROR_STATE();

        RAISERROR('Transcript generation failed: %s', @ErrSeverity, @ErrState, @ErrMsg);
    END CATCH
END;


EXEC GenerateStudentTranscript @StudentID = 5;