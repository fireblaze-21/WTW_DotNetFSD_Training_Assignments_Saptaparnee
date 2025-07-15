using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Student
    {
        public string Name { get; set; }
        public double[] Grades;
        public double AttendancePercentage { get; set; }
    }


    internal class StudentReportGeneratorBeforeRefactor
    {

            public static void GenerateStudentReport(Student student)
            {
            // Step 1: Validate Student Details
            if (student == null || string.IsNullOrWhiteSpace(student.Name) || student.Grades.Length == 0)
            {
                Console.WriteLine("Invalid Student Details.");
                return;
            }
            // Step 2: Calculate Average of Individual Grades of a Student
            double average = student.Grades.Average();

            // Step 3: Letter Grade Evaluation
            string letterGrade;
            if (average >= 90)
            {
                letterGrade = "A";
            }
            else if (average >= 80)
            {
                letterGrade = "B";
            }
            else if (average >= 70)
            {
                letterGrade = "C";
            }
            else if (average >= 60)
            {
                letterGrade = "D";
            }
            else
            {
               letterGrade= "F";
            }


            // Step 4: Attendance Status
            string attendanceStatus = student.AttendancePercentage >= 75 ? "Satisfactory" : "Unsatisfactory";


            // Step 5: Display Report

            Console.WriteLine("----- Student Report -----");
            Console.WriteLine($"Name: {student.Name}");
            Console.WriteLine($"Average Grade: {average:F2}");
            Console.WriteLine($"Letter Grade: {letterGrade}");
            Console.WriteLine($"Attendance: {attendanceStatus}");
            Console.WriteLine("--------------------------");


            }

        static void Main(string[] args)
            {

            Student student3 = new Student();
            student3.Name = "Scott";
            student3.Grades = new double[4];
            student3.Grades[0] = 89;
            student3.Grades[1] = 99;
            student3.Grades[2] = 100;
            student3.Grades[3] = 100;
            student3.AttendancePercentage = 100;

            Student student1 = new Student
            {
                Name = "Alice",
                Grades = new double[] { 85, 90, 78 },
                AttendancePercentage = 82
            };

            Student student2 = new Student
            {
                Name = "Bob",
                Grades = new double[] { 55, 60, 58 },
                AttendancePercentage = 70
            };
            GenerateStudentReport(student1);
            GenerateStudentReport(student2);
            GenerateStudentReport(student3);
            Console.ReadLine();
            }
        }
    }

