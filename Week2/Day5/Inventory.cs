using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    // Interface for inventory operations
    public interface IInventoryItem
    {
        void Restock(int quantity);
        void Sell(int quantity);
        string GetDetails();
    }

    // Base Product class
    public abstract class Product : IInventoryItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; protected set; }

        public Product(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public virtual void Restock(int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Restock quantity must be positive.");
                return;
            }

            Stock += quantity;
            Console.WriteLine($"{quantity} units of {Name} restocked.");
        }

        public virtual void Sell(int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Sale quantity must be positive.");
                return;
            }

            if (Stock >= quantity)
            {
                Stock -= quantity;
                Console.WriteLine($"{quantity} units of {Name} sold.");
            }
            else
            {
                Console.WriteLine($"Not enough stock to sell {quantity} units of {Name}.");
            }
        }

        public abstract string GetDetails();
    }

    // Electronic product class
    public class Electronic : Product
    {
        public int WarrantyMonths { get; set; }

        public Electronic(string name, decimal price, int stock, int warrantyMonths)
            : base(name, price, stock)
        {
            WarrantyMonths = warrantyMonths;
        }

        public override string GetDetails()
        {
            return $"[Electronic] {Name} - ${Price}, Stock: {Stock}, Warranty: {WarrantyMonths} months";
        }
    }

    // Grocery product class
    public class Grocery : Product
    {
        public DateTime ExpiryDate { get; set; }

        public Grocery(string name, decimal price, int stock, DateTime expiryDate)
            : base(name, price, stock)
        {
            ExpiryDate = expiryDate;
        }

        public override string GetDetails()
        {
            return $"[Grocery] {Name} - ${Price}, Stock: {Stock}, Expires on: {ExpiryDate:yyyy-MM-dd}";
        }
    }

    // Inventory manager class
    public class InventoryManager
    {
        private List<IInventoryItem> products = new List<IInventoryItem>();

        public void AddProduct(IInventoryItem item)
        {
            products.Add(item);
            Console.WriteLine($"{(item as Product)?.Name} added to inventory.");
        }

        public void RestockProduct(string name, int quantity)
        {
            var product = FindProduct(name);
            if (product != null)
                product.Restock(quantity);
            else
                Console.WriteLine($"Product '{name}' not found.");
        }

        public void SellProduct(string name, int quantity)
        {
            var product = FindProduct(name);
            if (product != null)
                product.Sell(quantity);
            else
                Console.WriteLine($"Product '{name}' not found.");
        }

        private IInventoryItem FindProduct(string name)
        {
            return products.FirstOrDefault(p => (p as Product)?.Name.Equals(name, StringComparison.OrdinalIgnoreCase) == true);
        }

        public void GenerateReport()
        {
            Console.WriteLine("\n=== Inventory Report ===");
            foreach (var item in products)
            {
                Console.WriteLine(item.GetDetails());

                if (item is Electronic electronic)
                {
                    Console.WriteLine($"-> Warranty: {electronic.WarrantyMonths} months");
                }
                else if (item is Grocery grocery)
                {
                    Console.WriteLine($"-> Expiry Date: {grocery.ExpiryDate:yyyy-MM-dd}");
                }
            }
            Console.WriteLine("========================\n");
        }
    }

    // Main application
    class Inventory
    {
        static void Main(string[] args)
        {
            InventoryManager inventory = new InventoryManager();

            // Example scenario:
            Electronic laptop = new Electronic("Laptop", 999.99m, 10, 24);
            Grocery milk = new Grocery("Milk", 3.99m, 50, new DateTime(2025, 8, 1));

            inventory.AddProduct(laptop);
            inventory.AddProduct(milk);

            inventory.RestockProduct("Laptop", 5);
            inventory.SellProduct("Laptop", 3);
            inventory.SellProduct("Milk", 10);

            inventory.GenerateReport();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
