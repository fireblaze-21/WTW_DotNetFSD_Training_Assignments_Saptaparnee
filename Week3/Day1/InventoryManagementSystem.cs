using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class InventoryManagementSystem
    {
        static void DisplayMenu()
        {
            Console.WriteLine("Inventory Manager");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Quantity");
            Console.WriteLine("3. View Inventory");
            Console.WriteLine("4. Exit");
        }

        static void AddProduct(List<string> productList, Dictionary<string, int> inventory)
        {
            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(productName))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }

            if (inventory.ContainsKey(productName))
            {
                Console.WriteLine("Product already exists.");
                return;
            }

            productList.Add(productName);
            inventory[productName] = 0;
            Console.WriteLine("Product added!");
        }

        static void UpdateQuantity(Dictionary<string, int> inventory)
        {
            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine()?.Trim();

            if (!inventory.ContainsKey(productName))
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("Enter Quantity to Add (can be negative): ");
            string qtyInput = Console.ReadLine()?.Trim();

            if (!int.TryParse(qtyInput, out int quantityChange))
            {
                Console.WriteLine("Invalid quantity. Please enter an integer.");
                return;
            }

            int currentQty = inventory[productName];
            int updatedQty = currentQty + quantityChange;
            if (updatedQty < 0)
            {
                updatedQty = 0;
            }

            inventory[productName] = updatedQty;
            Console.WriteLine("Quantity updated!");
        }

        static void ViewInventory(List<string> productList, Dictionary<string, int> inventory)
        {
            if (productList.Count == 0)
            {
                Console.WriteLine("No products in inventory.");
                return;
            }

            Console.WriteLine("Inventory:");
            foreach (var product in productList)
            {
                Console.WriteLine($"{product}: {inventory[product]} units");
            }
        }
        static void Main()
        {
            List<string> productList = new List<string>();
            Dictionary<string, int> productInventory = new Dictionary<string, int>();
            bool running = true;

            while (running)
            {
                DisplayMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddProduct(productList, productInventory);
                        break;
                    case "2":
                        UpdateQuantity(productInventory);
                        break;
                    case "3":
                        ViewInventory(productList, productInventory);
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a number between 1 and 4.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
