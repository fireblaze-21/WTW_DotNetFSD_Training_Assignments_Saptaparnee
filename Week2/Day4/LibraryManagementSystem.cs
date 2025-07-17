using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
   

    /*
     * - Create a base class `LibraryItem` to represent common attributes and behaviors of library items, 
such as:
 - Attributes: `Title` (string), `ItemId` (integer), `IsAvailable` (boolean, default true).
 - A method to display item details (e.g., title, ID, availability status).
 - Derive two classes, `Book` and `DVD`, from `LibraryItem`:
 - `Book` should include an additional attribute `Author` (string).
 - `DVD` should include an additional attribute `DurationMinutes` (integer).
 - Each derived class should override the base class method to provide specific details about the 
item (e.g., include author for books, duration for DVDs).*/
    class LibraryItem
    {
        public string Title { get; set; }
        public int ItemId { get; set; }
        public bool IsAvailable { get; set; }
        // Overridden later in derived classes
        public virtual void DisplayInfo()
        {
            Console.WriteLine($" Title : {Title}");
            Console.WriteLine($" ID  : {ItemId}");
            Console.WriteLine($"Availability Status : {(IsAvailable ? "Available" : "Not Available")}");

        }

    }
    class Book : LibraryItem
    {
        public string Author;
        public Book (string title, int itemId, string author)
        {
            // Validate title
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null, empty, or whitespace.", nameof(title));

            // Validate author
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be null, empty, or whitespace.", nameof(author));

            // Validate itemId
            if (itemId <= 0)
                throw new ArgumentOutOfRangeException(nameof(itemId), "Item ID must be a positive integer.");

            Title = title;
            ItemId = itemId;
            IsAvailable = true;
            Author = author;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"--------------------------------Book---------------------------");
            base.DisplayInfo();
            Console.WriteLine($" Author of the book : {Author}");
        }
        public void BorrowBook()
        {
            Console.WriteLine($"--------------------- Borrowing Book {Title}------------------------ ");
            IsAvailable = false;
        }
        public void ReturnBook()
        {
            Console.WriteLine($"--------------------- Returning Book {Title}------------------------ ");
            IsAvailable = true;
        }
    }
    class DVD : LibraryItem
    {
        public int DurationMinutes;
        public DVD (string title, int itemId, int durationMinutes)
        {
            // Validate title
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null, empty, or whitespace.", nameof(title));
            // Validate itemId
            if (itemId <= 0)
                throw new ArgumentOutOfRangeException(nameof(itemId), "Item ID must be a positive integer.");
            // Validate durationMinutes
            if (durationMinutes <= 0)
                throw new ArgumentOutOfRangeException(nameof(durationMinutes), "Duration minutes must be a positive integer.");
            Title = title;
            ItemId = itemId;
            IsAvailable = true;
            DurationMinutes = durationMinutes;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"------------------------DVD-------------------");
            base.DisplayInfo();
            Console.WriteLine($" Duration Of the DVD : {DurationMinutes} minutes");
        }
        public void BorrowDVD()
        {
            Console.WriteLine($"----------------Borrowing DVD {Title}-------------- ");
            IsAvailable = false;
        }
        public void ReturnDVD()
        {
            Console.WriteLine($"----------------Returning DVD {Title}-------------- ");
            IsAvailable = true;
        }
    }
    /*2. Borrowing Rules:
 - Create an abstract class `BorrowingRule` to define the structure for borrowing policies:
 - Attribute: `RuleName` (string).
 - An abstract method `CalculateMaxBorrowingDays()` to return the maximum number of days an
item can be borrowed.
 - A non-abstract method to display rule information.
 - Derive two classes from `BorrowingRule`:
 - `BookBorrowingRule`: Specifies that books can be borrowed for 14 days.
 - `DVDBorrowingRule`: Specifies that DVDs can be borrowed for 7 days*/
    abstract class BorrowingRule
    {
        public string RuleName { get; set; }
        //Abstract class constructor later instantiated from derived classes
        public BorrowingRule(string ruleName)
        {
            RuleName = ruleName;
        }
        //Abstract Method to return maximum number of borrowing days
        public abstract int CalculateMaxBorrowingDays();
        //Concrete Method to Display Rule Information
        public void DisplayRuleInfo()
        {
            Console.WriteLine($" Borrowing Rule : {RuleName}");
            Console.WriteLine($" Maximum Number Of Borrowing Days : {CalculateMaxBorrowingDays()} days ");
        }

    }
    class BookBorrowingRule : BorrowingRule
    {
        public BookBorrowingRule (string ruleName) : base(ruleName) { }
        public override int CalculateMaxBorrowingDays()
        {
            return 14;
        }
    }
    class DVDBorrowingRule : BorrowingRule
    {
        public DVDBorrowingRule (string ruleName) : base(ruleName) { }
        public override int CalculateMaxBorrowingDays()
        {
            return 7;
        }
    }
    

    internal class LibraryManagementSystem
    {
   
        static void Main(string[] args)
        {
            Book book1 = new Book("The Great Gatsby", 1001, "F. Scott Fitzgerald");
            DVD dvd1 = new DVD(" Inception", 1002, -148);
            book1.DisplayInfo();
            dvd1.DisplayInfo();
            book1.BorrowBook();
            book1.DisplayInfo();
            dvd1.DisplayInfo();
            book1.ReturnBook();
            dvd1.BorrowDVD();
            book1.DisplayInfo();
            dvd1.DisplayInfo();
            Console.WriteLine($"--------------Book Borrowing Rule----------------------");
            BookBorrowingRule borrowbook1 = new BookBorrowingRule("Standard Book Rule");
            borrowbook1.DisplayRuleInfo();
            Console.WriteLine($"--------------DVD Borrowing Rule----------------------");
            DVDBorrowingRule borrowdvd1 = new DVDBorrowingRule("Standard Dvd Rule");
            borrowdvd1.DisplayRuleInfo();



        }
    }
}
