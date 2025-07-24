using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public delegate void GreetingType(string message);

    class MessageType
    {
        public void InformalGreet(string message)
        {
            Console.WriteLine($"Hey {message} , what's up!!!");
        }
        public void FormalGreet(string message)
        {
            Console.WriteLine($" Dear {message} , it's a pleasure to meet you");
        }
    }
    internal class GreetingMessage
    {
        static void Main()
        {
            MessageType mt = new MessageType();
            Console.WriteLine($"Enter the greeting type");
            string ans = Console.ReadLine();
            Console.WriteLine($"Enter the User name");
            string name = Console.ReadLine();
            GreetingType gt;
            if (string.Equals(ans,"FORMAL", StringComparison.OrdinalIgnoreCase))
            {
                gt= new GreetingType(mt.FormalGreet);
               
            }
            else
            {
             gt= new GreetingType(mt.InformalGreet);
               
            }
            gt(name);
            Console.ReadLine();
        }
    }
}
