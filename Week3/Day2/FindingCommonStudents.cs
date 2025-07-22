using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class FindingCommonStudents
    {
        static HashSet<string> ReadStudentsFromLines()
        {
            HashSet<string> students = new HashSet<string>();
            while (true)
            {
                string line = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(line))
                    break;
                students.Add(line);
            }
            return students;
        }
        static void Main()
        {
            Console.WriteLine("Enter student names enrolled in Math course (one per line). Press Enter on empty line to finish:");
            HashSet<string> mathStudents = ReadStudentsFromLines();

            Console.WriteLine("\nEnter student names enrolled in Science course (one per line). Press Enter on empty line to finish:");
            HashSet<string> scienceStudents = ReadStudentsFromLines();

            HashSet<string> commonStudents = new HashSet<string>(mathStudents);
            commonStudents.IntersectWith(scienceStudents);

            if (commonStudents.Count > 0)
            {
                Console.WriteLine("\nStudents enrolled in both Math and Science:\n");
                foreach (var student in commonStudents)
                {
                    Console.WriteLine(student);
                }
                Console.WriteLine($"\nTotal students in both courses: {commonStudents.Count}");
            }
            else
            {
                Console.WriteLine("\nThere are no students enrolled in both Math and Science courses.");
            }
        }
    }
}
