using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class configurationManager
    {
        
        // Compile-time constant (fixed value)
        public const int MaximumConnections = 100;
         // Runtime-initialized readonly variable
        public  readonly string ApiKey;
        public configurationManager(string apiKey)
        {
           ApiKey = apiKey;
        }
        public void display()
        {
            Console.WriteLine($"Maximum Connections: {MaximumConnections}"); 
        }
    }

    class config
    {

        static void Main()
        {

            configurationManager obj1 = new configurationManager("ABC123");
            obj1.display();
            Console.WriteLine($"API Key: {obj1.ApiKey}");

            Console.WriteLine("--------------------------------------");

            configurationManager obj2 = new configurationManager("XYZ789");
            obj2.display();
            Console.WriteLine($"API Key: {obj2.ApiKey}");


            Console.ReadLine();
        }
    }
}