CREATE DATABASE CityCareDB;

USE CityCareDB;

/*Database Schema: 
o Patients Table: Store patient details (PatientID, FirstName, LastName, DateOfBirth, 
Gender, ContactNumber).
o Doctors Table: Store doctor details (DoctorID, DoctorName, Specialty, Department).
o Appointments Table: Store appointment details (AppointmentID, PatientID, 
DoctorID, AppointmentDate, Reason, Status).
o Performance Tuning: 
? Create indexes on frequently queried columns (e.g., PatientID, DoctorID, 
AppointmentDate, Status) to speed up searches and joins.
? Use clustered indexes on primary keys and non-clustered indexes on foreign 
keys and filter columns*/

-- Create Patients table
CREATE TABLE Patients (
    PatientID INT IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    DateOfBirth DATE,
    Gender CHAR(1),
    ContactNumber VARCHAR(15)
);

-- Create Doctors table
CREATE TABLE Doctors (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    DoctorName NVARCHAR(100),
    Specialty NVARCHAR(50),
    Department NVARCHAR(50)
);

-- Create Appointments table
CREATE TABLE Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    AppointmentDate DATE,
    Reason NVARCHAR(255),
    Status VARCHAR(20)
);
--Indexes for Performance Tuning

-- Non-clustered indexes on frequently filtered and joined columns
CREATE NONCLUSTERED INDEX IX_Appointments_PatientID
ON Appointments(PatientID);

CREATE NONCLUSTERED INDEX IX_Appointments_DoctorID
ON Appointments(DoctorID);

CREATE NONCLUSTERED INDEX IX_Appointments_AppointmentDate
ON Appointments(AppointmentDate);

CREATE NONCLUSTERED INDEX IX_Appointments_Status
ON Appointments(Status);


/*DML Operations: 
o Insert, Update, and Delete records for patients, doctors, and appointments.
o Performance Tuning: 
? Use batch inserts for sample data to reduce transaction overhead.
? Ensure updates and deletes are targeted with precise WHERE clauses to 
avoid table scans.
? Monitor index fragmentation and rebuild indexes if needed*/

-- Batch insert Patients
INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, ContactNumber)
VALUES
('John', 'Doe', '1990-01-01', 'M', '1234567890'),
('Jane', 'Smith', '1985-05-20', 'F', '0987654321'),
('Alice', 'Brown', '2000-03-15', 'F', '5556667777');

-- Batch insert Doctors
INSERT INTO Doctors (DoctorName, Specialty, Department)
VALUES
('Dr. Alan Grant', 'Cardiology', 'Heart'),
('Dr. Ellie Sattler', 'Neurology', 'Brain');

-- Batch insert Appointments
INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Reason, Status)
VALUES
(1, 1, '2025-08-15', 'Regular Checkup', 'Scheduled'),
(2, 2, '2025-08-16', 'Migraine', 'Completed'),
(3, 1, '2025-08-17', 'Chest Pain', 'Cancelled');

-- Update appointment status for a specific patient and appointment
UPDATE Appointments
SET Status = 'Completed'
WHERE AppointmentID = 1 AND PatientID = 1;

SELECT * FROM Appointments;

-- Delete a specific doctor (if no appointments exist)
DELETE FROM Doctors
WHERE DoctorID = 2
  AND NOT EXISTS (
      SELECT 1 FROM Appointments WHERE DoctorID = 2
  );

SELECT * FROM Doctors;

  -- Rebuild heavily fragmented indexes (example for Appointments table)
ALTER INDEX ALL ON Appointments
REBUILD WITH (FILLFACTOR = 90, SORT_IN_TEMPDB = ON);


/*. Joins: 
o Use INNER JOIN and LEFT JOIN to combine data for reports.
o Performance Tuning: 
? Ensure joins use indexed columns (e.g., PatientID, DoctorID).
? Avoid unnecessary columns in SELECT statements to reduce I/O.
? Use query execution plans to identify and optimize costly join operations*/

--INNER JOIN: Appointments with Patient & Doctor Info

SELECT 
    a.AppointmentID,
    p.FirstName + ' ' + p.LastName AS PatientName,
    d.DoctorName,
    a.AppointmentDate,
    a.Status
FROM Appointments a
INNER JOIN Patients p ON a.PatientID = p.PatientID
INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
WHERE a.Status = 'Scheduled';

--LEFT JOIN: Show All Patients & Any Scheduled Appointments

SELECT 
    p.PatientID,
    p.FirstName + ' ' + p.LastName AS PatientName,
    a.AppointmentDate,
    a.Status
FROM Patients p
LEFT JOIN Appointments a ON p.PatientID = a.PatientID
  AND a.Status = 'Scheduled';


