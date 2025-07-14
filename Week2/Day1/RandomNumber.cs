using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class RandomNumber
    {
        static void Main()
        {
            Random rand = new Random();
            int[] numbers = new int[20];
            int noOfEven = 0, noOfOdd = 0;
            Console.WriteLine("20 Random Numbers:");
            for (int i = 0; i < 20; i++)
            {
                numbers[i] = rand.Next(); // Generates a non-negative random integer
                Console.WriteLine($"Number {i + 1}: {numbers[i]}");
                if (numbers[i] % 2 == 0)
                {
                    noOfEven += 1;
                }
                else
                {
                    noOfOdd += 1;
                }
            }
            Console.WriteLine($"Final Report -----------");
            Console.WriteLine($"Number of EvenNumbers : {noOfEven}");
            Console.WriteLine($"Number of OddNumbers : {noOfOdd}");
        }

    }
}
