using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public delegate void MathFunction(int x, int y);
    internal class MathematicalOps
    {
       static void Main()
        {
            MathFunction add = (x,y) =>
                Console.WriteLine($"The Addition Result is : {x + y}");
            MathFunction subtract = (x, y) =>
                Console.WriteLine($"The Subtraction Result is : {x - y}");
            MathFunction multiply = (x, y) =>
                Console.WriteLine($"The Multiplication Result is : {x * y}");
            int num1, num2;

            // Input and validation for first number
            Console.Write("Enter the first integer: ");
            if (!int.TryParse(Console.ReadLine(), out num1))
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
                return;
            }

            // Input and validation for second number
            Console.Write("Enter the second integer: ");
            if (!int.TryParse(Console.ReadLine(), out num2))
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
                return;
            }

            // Prompt for operation
            Console.WriteLine("Choose an operation: add, subtract, multiply");
            string operation = Console.ReadLine().ToLower();
            MathFunction mathFunc;
            if (operation == "multiply")
            {
                mathFunc = multiply;
            }
            else if (operation =="subtract")
            {
                mathFunc = subtract;

            }
            else
            {
                mathFunc = add;
            }
            mathFunc(num1, num2);
        }

    }
}
