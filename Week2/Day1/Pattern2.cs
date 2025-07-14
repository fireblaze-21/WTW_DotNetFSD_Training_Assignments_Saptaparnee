using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Pattern2
    {
        static void Main(string[] args)
        {
            Console.WriteLine($" Enter the number of Rows : ");
            int row = int.Parse(Console.ReadLine());
            for (int i = row; i >= 1; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write($"{(char)(96+j)} ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
