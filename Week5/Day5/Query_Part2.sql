/*
This query text was retrieved from showplan XML, and may be truncated.
*/

SELECT 
    p.PatientID,
    p.FirstName + ' ' + p.LastName AS PatientName,
    a.AppointmentDate,
    a.Status
FROM Patients p
LEFT JOIN Appointments a ON p.PatientID = a.PatientID
  AND a.Status = 'Scheduled'



  ---------------------------------------------------------------------------------------

  -- Filter appointments from August 1, 2025, with 'Scheduled' status in 'Cardiology' department

SELECT 
    a.AppointmentID,
    p.FirstName + ' ' + p.LastName AS PatientName,
    d.DoctorName,
    a.AppointmentDate,
    a.Status,
    d.Department
FROM Appointments a
INNER JOIN Patients p ON a.PatientID = p.PatientID
INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
WHERE 
    a.AppointmentDate >= '2025-08-01'     -- SARGABLE (no function on column)
    AND a.Status = 'Scheduled'
    AND d.Department = 'Cardiology';


	CREATE NONCLUSTERED INDEX IX_Appointments_Date_Status
ON Appointments(AppointmentDate, Status);


----------------------------------------------------------------------------------------

SELECT 
    d.Department,
    COUNT(*) AS TotalAppointments,
    SUM(CASE WHEN a.Status = 'Completed' THEN 1 ELSE 0 END) AS CompletedAppointments
FROM Appointments a
INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
GROUP BY d.Department
ORDER BY d.Department;

CREATE NONCLUSTERED INDEX IX_Appointments_Doctor_Status
ON Appointments(DoctorID, Status);

------------------------------------------------------------------------------------------------------

--Stored Procedure: ScheduleAppointment


CREATE PROCEDURE dbo.ScheduleAppointment
    @PatientID INT,
    @DoctorID INT,
    @AppointmentDate DATETIME,
    @Reason NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    -- Use TRY-CATCH for error handling
    BEGIN TRY
        -- Use READ COMMITTED isolation to minimize locking
        SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
        BEGIN TRANSACTION;

        -- Insert appointment with parameterized values
        INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Reason, Status)
        VALUES (@PatientID, @DoctorID, @AppointmentDate, @Reason, 'Scheduled');

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback on error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Return error info
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

EXEC dbo.ScheduleAppointment
    @PatientID = 1,
    @DoctorID = 1,
    @AppointmentDate = '2025-08-15 10:30:00',
    @Reason = 'Routine Checkup';



	--------------------------------------------------------------------------------

	CREATE FUNCTION dbo.CalculatePatientAge (@DateOfBirth DATE)
RETURNS INT
WITH SCHEMABINDING
AS
BEGIN
    RETURN DATEDIFF(YEAR, @DateOfBirth, GETDATE()) 
           - CASE 
                 WHEN MONTH(@DateOfBirth) > MONTH(GETDATE())
                      OR (MONTH(@DateOfBirth) = MONTH(GETDATE()) AND DAY(@DateOfBirth) > DAY(GETDATE()))
                 THEN 1 ELSE 0 
             END;
END;
GO

SELECT 
    PatientID,
    FirstName,
    LastName,
    dbo.CalculatePatientAge(DateOfBirth) AS Age
FROM Patients
WHERE dbo.CalculatePatientAge(DateOfBirth) >= 60;

--Monitor Index Usage

SELECT 
    OBJECT_NAME(i.object_id) AS TableName,
    i.name AS IndexName,
    i.index_id,
    user_seeks,
    user_scans,
    user_lookups,
    user_updates
FROM sys.dm_db_index_usage_stats s
JOIN sys.indexes i ON s.object_id = i.object_id AND s.index_id = i.index_id
WHERE database_id = DB_ID()
ORDER BY user_seeks + user_scans + user_lookups DESC;

---------------------------------------------------------------------------------------
SELECT 
    p.PatientID,
    p.FirstName + ' ' + p.LastName AS PatientName,
    dbo.CalculatePatientAge(p.DateOfBirth) AS Age,
    d.DoctorName,
    d.Department,
    a.AppointmentDate,
    a.Reason,
    a.Status
FROM Appointments a
INNER JOIN Patients p ON a.PatientID = p.PatientID
INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
WHERE 
    a.Status = 'Scheduled'
    AND d.Department = 'Cardiology'
    AND dbo.CalculatePatientAge(p.DateOfBirth) >= 60
    AND a.AppointmentDate >= '2025-08-01'
ORDER BY a.AppointmentDate;

-- Appointments: Filtered, joined, and selected columns
CREATE NONCLUSTERED INDEX IX_Appointments_Covering
ON Appointments(AppointmentDate, Status, DoctorID, PatientID)
INCLUDE (Reason);

-- Doctors: For department filter and name
CREATE NONCLUSTERED INDEX IX_Doctors_Department
ON Doctors(Department)
INCLUDE (DoctorName);

-- Patients: For join and UDF use on DateOfBirth
CREATE NONCLUSTERED INDEX IX_Patients_DateOfBirth
ON Patients(DateOfBirth)
INCLUDE (FirstName, LastName);