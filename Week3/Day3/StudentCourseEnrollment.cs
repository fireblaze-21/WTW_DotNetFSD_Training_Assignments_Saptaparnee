using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    // 1. Define Student class
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // 2. Define Enrollment record
    class Enrollment
    {
        public string CourseName { get; set; }
        public Student Student { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
    internal class StudentCourseEnrollment
    {
        static void EnrollStudent(List<Enrollment> enrollments, Student student, string courseName)
        {
            bool alreadyEnrolled = enrollments.Any(e =>
                e.Student.Id == student.Id && e.CourseName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

            if (!alreadyEnrolled)
            {
                enrollments.Add(new Enrollment
                {
                    Student = student,
                    CourseName = courseName
                });
            }
        }
        static void Main(string[] args)
        {
            // Student List
            List<Student> students = new List<Student>
            {
                new Student { Id = 1, Name = "Alice" },
                new Student { Id = 2, Name = "Bob" },
                new Student { Id = 3, Name = "Charlie" }
            };

            // Unique course list
            HashSet<string> offeredCourses = new HashSet<string>
            {
                "Mathematics", "Physics", "Chemistry", "Computer Science"
            };

            // Enrollment list
            List<Enrollment> enrollments = new List<Enrollment>();
            // Enroll students
            EnrollStudent(enrollments, students[0], "Mathematics");
            EnrollStudent(enrollments, students[0], "Physics");
            EnrollStudent(enrollments, students[1], "Mathematics");
            EnrollStudent(enrollments, students[2], "Computer Science");
            EnrollStudent(enrollments, students[1], "Chemistry");

            // Attempt duplicate enrollment (should be ignored)
            EnrollStudent(enrollments, students[0], "Mathematics");

            // Output
            Console.WriteLine("===== All Offered Courses =====");
            foreach (var course in offeredCourses)
                Console.WriteLine(course);
            Console.WriteLine("=======Student Details======");
            foreach (var student in students)
            {
                Console.WriteLine($"Student Id : {student.Id} , Student Name : {student.Name}");
            }
                

            Console.WriteLine("\n===== Students Grouped by Course =====");
            var groupedByCourse = enrollments
                .GroupBy(e => e.CourseName);

            foreach (var group in groupedByCourse)
            {
                Console.WriteLine($"\nCourse: {group.Key}");
                foreach (var e in group)
                {
                    Console.WriteLine($" - {e.Student.Name} with Id: {e.Student.Id} (Enrolled on: {e.EnrollmentDate.ToShortDateString()})");
                }
            }

            Console.WriteLine("\n===== Courses Enrolled by Alice =====");
            var aliceCourses = enrollments
                .Where(e => e.Student.Name == "Alice")
                .Select(e => e.CourseName);

            foreach (var course in aliceCourses)
                Console.WriteLine(course);

            Console.ReadLine();
        }
    }
}
