using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class TrackingUniqueVisitors
    {
        static void Main()
        {
            HashSet<string> uniqueVisitors = new HashSet<string>();

            Console.WriteLine("Enter visitor IDs (press Enter on an empty line to finish):");

            while (true)
            {
                string visitorId = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(visitorId))
                    break;

                if (uniqueVisitors.Contains(visitorId))
                {
                    Console.WriteLine($"Repeat visitor: {visitorId}");
                }
                else
                {
                    uniqueVisitors.Add(visitorId);
                    Console.WriteLine($"New visitor: {visitorId}");
                }
            }
            Console.WriteLine($"----------------------------------------------------");
            Console.WriteLine($"Total unique visitors: {uniqueVisitors.Count}");
        }
    }
}
