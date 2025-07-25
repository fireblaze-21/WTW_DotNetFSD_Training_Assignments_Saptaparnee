using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    /*1. Student Class:

- Create a `Student` class to represent a student record.

- Properties:

- `Id` (int): A unique identifier for the student (read-only after initialization).

- `Name` (string): The student's name (read-only after initialization).

- `Grade` (double): The student's average grade (0.0 to 100.0, read-only after initialization).

- Ensure properties are encapsulated (e.g., use private setters or equivalent to prevent external modification after object creation).*/
    public class Students
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Grade { get; private set; }
        public Students(int id,string name,double grade)
        {
            this.Id = id;
            this.Name = name;
            this.Grade = grade;
        }

    }
    /*2. StudentManager Class:

- Create a `StudentManager` class to manage student records.

- Collections:

- Use a `Dictionary<int, Student>` to store student records, where the key is the student’s ID and the value is a `Student` object.

- 
Optionally, use a `List<Student>` internally (or rely on the `Dictionary`’s `Values` collection) for iterating over students.
- Methods:

- `AddStudent(int id, string name, double grade)`: Adds a new student to the collection.

- `GetStudentById(int id)`: Retrieves a student by their ID.

- `DisplayAllStudents()`: Displays all students in the system, including their ID, name, and grade.

- Validation:

- Ensure student IDs are unique; throw an `ArgumentException` if an ID already exists.

- Validate that the student’s name is not null or empty; throw an `ArgumentException` if invalid.

- Ensure the grade is between 0.0 and 100.0; throw an `ArgumentException` if out of range.

- Throw a `KeyNotFoundException` if a requested student ID does not exist in the `GetStudentById` method.*/
    public class StudentManager
    {
        Dictionary<int,Students> dict = new Dictionary<int,Students>();
        public void AddStudent(int id, string name, double grade)
        {
            if(dict.ContainsKey(id)==true)
            {
                throw new ArgumentException("ID already exists");
            }
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name should not be null or empty");
            }
            if(grade<0 || grade>100)
            {
                throw new ArgumentException("Grade is out of given range");
            }
            Console.WriteLine($" Adding student with Id : {id} , Name ; {name} , Grade : {grade}");
            Students s = new Students(id, name, grade);
            dict[id] = s;

        }
        public void GetStudentById(int id)
        {
            if(dict.ContainsKey(id))
            {
                Students retrievedStudent = dict[id];
                Console.WriteLine($" Id {id} has been found. Student Name : {retrievedStudent.Name} , Grade :{retrievedStudent.Grade} ");
            }
            else
            {
                throw new KeyNotFoundException("Invalid student ID");
            }
        }
        public void DisplayStudent()
        {
            Console.WriteLine(" Student details are as follows : ");
            foreach (var kvp in dict)
            {
                Console.WriteLine($" Id : {kvp.Key} , Name : {kvp.Value.Name} , Grade : {kvp.Value.Grade}");
            }
        }
    }
    internal class StudentClassMAnagement
    {
        static void Main()
        {
            /*- In the `Main` method, create a `StudentManager` instance and demonstrate the following test cases:

- Add at least three students with valid IDs, names, and grades.

- Attempt to add a student with a duplicate ID (should throw an exception).

- Attempt to add a student with an invalid grade (e.g., -10 or 150).

- Retrieve and display a student by ID (both valid and invalid cases).

- Display all students in the system.

- Use try-catch blocks to handle exceptions and display user-friendly error messages.*/
            StudentManager sm = new StudentManager();
            try
            {

                sm.AddStudent(1, "Saptaparnee", 99);
                sm.AddStudent(2, "Alex", 100);
                sm.AddStudent(3, "Bob", 77);
                try
                {
                    Console.WriteLine($" Adding same Id again ");
                    sm.AddStudent(1, "Saptaparnee", 99);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($" Student detail error : {ex.Message}");
                }
                try
                {
                    Console.WriteLine($" Adding wrong Grade ");
                    sm.AddStudent(4, "Saptaparnee", -99);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($" Student detail error : {ex.Message}");
                }
                
                try
                {
                    Console.WriteLine("Getting student with Id 1");
                    sm.GetStudentById(1);
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($" Student Retrieval Id error: {ex.Message}");
                }
                try
                {
                    Console.WriteLine("Getting student with Wrong Id");
                    sm.GetStudentById(10);
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($" Student Retrieval Id error: {ex.Message}");
                }
                
                sm.DisplayStudent();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($" Student detail error : {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($" Student Retrieval Id error: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($" Other error : {ex.Message}");
            }

        }
    }
}
