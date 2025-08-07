CREATE PROCEDURE EnrollStudentInCourse
    @StudentID INT,
    @CourseID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @PrereqCourseID INT;
        DECLARE @EnrolledCount INT;
        DECLARE @MaxCapacity INT;

        -- Check if student already enrolled in the course
        IF EXISTS (
            SELECT 1 FROM StudentCourses 
            WHERE StudentID = @StudentID AND CourseID = @CourseID
        )
        BEGIN
            RAISERROR('Enrollment failed: Student is already enrolled in the course.', 16, 1);
            RETURN;
        END

        -- Get prerequisite course ID (if any)
        SELECT @PrereqCourseID = PrerequisiteCourseID
        FROM Courses
        WHERE CourseID = @CourseID;

        -- If a prerequisite exists, check if student has completed it
        IF @PrereqCourseID IS NOT NULL
        BEGIN
            IF NOT EXISTS (
                SELECT 1 FROM StudentCourses
                WHERE StudentID = @StudentID
                  AND CourseID = @PrereqCourseID
                  AND Status = 'Completed'
            )
            BEGIN
                RAISERROR('Enrollment failed: Prerequisite course not completed.', 16, 1);
                RETURN;
            END
        END

        -- Get number of currently enrolled students in the course
        SELECT @EnrolledCount = COUNT(*) 
        FROM StudentCourses
        WHERE CourseID = @CourseID AND Status = 'Enrolled';

        -- Get course's max capacity
        SELECT @MaxCapacity = MaxCapacity
        FROM Courses
        WHERE CourseID = @CourseID;

        IF @EnrolledCount >= @MaxCapacity
        BEGIN
            RAISERROR('Enrollment failed: Course is at full capacity.', 16, 1);
            RETURN;
        END

        -- All checks passed: Insert into StudentCourses (enroll student)
        INSERT INTO StudentCourses (StudentID, CourseID, Status)
        VALUES (@StudentID, @CourseID, 'Enrolled');

        PRINT 'Enrollment successful.';
    END TRY

    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR('Enrollment failed: %s', @ErrorSeverity, @ErrorState, @ErrorMessage);
    END CATCH
END;


EXEC EnrollStudentInCourse @StudentID = 2, @CourseID = 1;
EXEC EnrollStudentInCourse @StudentID = 5, @CourseID = 1;


EXEC EnrollStudentInCourse @StudentID = 1, @CourseID = 1;
EXEC EnrollStudentInCourse @StudentID = 2, @CourseID = 3;
EXEC EnrollStudentInCourse @StudentID = 4, @CourseID = 3;