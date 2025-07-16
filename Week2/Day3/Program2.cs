using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

        public bool IsAvailable { get; set; }


        public Book (string title, string author, string isbn)
        {
            this.Title = title;
            this.Author = author;
            this.ISBN = isbn;
            this.IsAvailable = true;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"----------------------Book Details-----------------");
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Author: " + Author);
            Console.WriteLine("ISBN: " + ISBN);
            Console.WriteLine("Available: " + (IsAvailable ? "Yes" : "No"));
            Console.WriteLine("----------------------------------------------------");
        }

        public void BorrowBook()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                Console.WriteLine($"Successfully borrowed \"{Title}\".");
            }
            else
            {
                Console.WriteLine($"The \"{Title}\" is currently not available.");
            }
        }

    }
    
    internal class Program2
    {
        static void Main(string[] args)
        {
            Book book1 = new Book("1984", "George Orwell", "123456789");
            Book book2 = new Book("Pride and Prejudice", "Jane Austen", "987654321");

            book1.DisplayInfo();
            book2.DisplayInfo();

            Console.WriteLine($"-------------------Borrow first book-----------------------");
            book1.BorrowBook();
            book1.DisplayInfo();
            Console.WriteLine($"-------------------Borrow second book-----------------------");
            book2.BorrowBook();
            book2.DisplayInfo();
            Console.WriteLine($"-------------------Borrow first book AGAIN-----------------------");
            book1.BorrowBook();
            book1.DisplayInfo();

            Console.ReadLine();
        }
    }
}

