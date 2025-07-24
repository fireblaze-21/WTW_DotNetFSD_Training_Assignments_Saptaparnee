using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public delegate bool ValidateOrder(Order order);
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

    }
    internal class OrderProcessing
    {
        static void ProcessOrder (Order order)
        {
            bool isAmountValid = false;
            bool isIdValid = false;
            ValidateOrder c1 = (o) =>
            {
                if (o.Amount > 5)
                {
                    Console.WriteLine("Order amount is valid.");
                    isAmountValid = true;
                    return true;
                }
                else
                {
                    
                    Console.WriteLine("Order amount is too low...");
                    return false;
                }
            };
            ValidateOrder c2 = (o) =>
            {
                if (o.Id > 0)
                {
                    isIdValid = true;
                    Console.WriteLine("Order ID is valid.");
                    return true;
                }
                else
                {
                    
                    Console.WriteLine("Order ID must be greater than 0.");
                    return false;
                }
            };
            ValidateOrder val = c1;
            val += c2;
            val(order);

           

            if (isAmountValid && isIdValid)
            {
                Console.WriteLine("Order processed.");
            }
            else
            {
                Console.WriteLine("Order validation failed");
            }
            Console.WriteLine("-------------------------------------------------------");

        }
        static void Main()
        {
            Order order1 = new Order { Id = 101, Amount = 50 };
            ProcessOrder(order1);
            Order order2 = new Order { Id = -1, Amount = 50 };
            ProcessOrder(order2);
            Order order3 = new Order { Id = 101, Amount = 3 };
            ProcessOrder(order3);
            Order order4 = new Order { Id = -5, Amount = 1 };
            ProcessOrder(order4);
        }
    }
}
