using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
     class EmployeeDatabase
    {

        static void DisplayResult( int employeeId,string employeeName, string employeeJob, string? terminationDate = default)
        {
            string name = employeeName;
            int id = employeeId;
            string job = employeeJob;
            
            string displayDate = terminationDate ?? "";    

            Console.WriteLine($"Employee-ID: {id}");
            Console.WriteLine($"Employee-Name: {name}");
            Console.WriteLine($"Employee-Job: {job}");



            if (!string.IsNullOrEmpty(displayDate))
            {
                Console.WriteLine($"Employee Termination Date : {displayDate}");
            }
            else
            {
                Console.WriteLine("Active Employee.");
            }

            Console.WriteLine("------------------------------");

        }
        

        static void Main()
        {

            DisplayResult(1,"Scott","CEO", "1.1.2025");
            DisplayResult(2,"Smith","Analyst","");
            DisplayResult(3,"Sam","Manager" ,"3.6.2025");
            DisplayResult(4,"Bob","Teacher","");
           
            Console.ReadLine();
        }
    }
}