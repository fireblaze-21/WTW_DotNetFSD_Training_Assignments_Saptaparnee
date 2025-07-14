using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Fibonacci
    {
        static void Main(string[] args)
        {

            int a = 0, b = 1;

            Console.WriteLine("Fibonacci series between 0 and 100:");
            while (a <= 100)
            {
                Console.Write(a + " ");
                int temp = a;
                a = b;
                b = temp + b;
            }

            Console.WriteLine();
            Console.ReadLine();

        }
    }
}
