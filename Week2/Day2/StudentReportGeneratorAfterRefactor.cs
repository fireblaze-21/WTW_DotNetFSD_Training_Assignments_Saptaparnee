using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    internal class StudentReportGeneratorAfterRefactor
    {

        public static void GenerateStudentReport(Student student)
        {
            // Step 1: Validate Student Details

            if (!ValidateStudent(student))
            {
                Console.WriteLine("------------------------------- Student Report ---------------------------");
                Console.WriteLine("Invalid student data.");
                return;
            }
            //Used out keyword
            CalculateAverageGrade(student.Grades, out double average);
            string letterGrade = EvaluateLetterGrade(average);
            string attendanceStatus = AnalyzeAttendance(student.AttendancePercentage);

            DisplayStudentResult(student, average, letterGrade, attendanceStatus);
            // Detect Failing Grades 
            DetectFailingGrades(average);
            // Detect Pass / Fail for Student
            DetectPassOrFail(student, average);
        }

        static bool ValidateStudent(Student student)
        {
            return student != null &&
                   !string.IsNullOrWhiteSpace(student.Name) &&
                   student.Grades != null &&
                   student.Grades.Length > 0;
        }

        static void CalculateAverageGrade(double[] grades, out double average)
        {
            average=grades.Average();
        }


        static string EvaluateLetterGrade(double average)
        {
            if (average >= 90) return "A";
            if (average >= 80) return "B";
            if (average >= 70) return "C";
            if (average >= 60) return "D";
            return "F";
        }
        // Optional Parameter
        static string AnalyzeAttendance(double attendancePercentage =0)
        {
            return attendancePercentage >= 75 ? "Satisfactory" : "Unsatisfactory";
        }

        static void DisplayStudentResult(Student student, double average , string letterGrade, string attendanceStatus)
        {
            Console.WriteLine("------------------------------- Student Report ---------------------------");
            Console.WriteLine($"Name: {student.Name}");
            Console.WriteLine($"Average Grade: {average:F2}");
            Console.WriteLine($"Letter Grade: {letterGrade}");
            Console.WriteLine($"Attendance: {attendanceStatus}");
            
        }

        static void DetectFailingGrades(double average)
        {
            if (average < 40)
            {
                Console.WriteLine($"FAILING GRADES DETECTED ");
            }
        }

        static void DetectPassOrFail( Student student,double average)
        {
            if(average>=60 && student.AttendancePercentage>=75 )
            {
                Console.WriteLine($"Report Status : PASS ");
            }
            else
            {
                Console.WriteLine($"Report Status : FAIL ");
            }
        }
        static void Main(string[] args)
        {

            Student student1 = new Student
            {
                Name = "Alice",
                Grades = new double[] { 85, 90, 78 },
                AttendancePercentage = 82
            };

            Student student2 = new Student
            {
                Name = "Bob",
                Grades = new double[] { 34, 23, 12 },
                AttendancePercentage = 70
            };
            Student student3 = new Student();
            student3.Name = "Scott";
            student3.Grades = new double[4];
            student3.Grades[0] = 89;
            student3.Grades[1] = 99;
            student3.Grades[2] = 100;
            student3.Grades[3] = 100;
            student3.AttendancePercentage = 100;
            GenerateStudentReport(student1);
            GenerateStudentReport(student2);
            GenerateStudentReport(student3);
            Console.ReadLine();
        }
    }
}

