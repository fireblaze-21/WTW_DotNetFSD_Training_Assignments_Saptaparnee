using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{


    internal class Multiplication
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter the number :  ");
            int num = int.Parse(Console.ReadLine());


            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($" {num} * {i + 1} = {(num * (i + 1))}");

            }

            Console.ReadLine();
        }
    }
}

