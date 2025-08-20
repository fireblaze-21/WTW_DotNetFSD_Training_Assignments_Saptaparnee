using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    // -----------------------------------------
    // 1. Single Responsibility Principle (SRP)
    // -----------------------------------------
    public class Books
    {
        public string Title { get; }
        public string Author { get; }
        public string ISBN { get; }

        public Books(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
        }
    }

    public class BookRepository
    {
        public void Save(Books book)
        {
            Console.WriteLine($"[SRP] Saving '{book.Title}' to the database.");
        }
    }

    public class BookPrinter
    {
        public void Print(Books book)
        {
            Console.WriteLine("[SRP] Printing Book Info:");
            Console.WriteLine($"Title: {book.Title}\nAuthor: {book.Author}\nISBN: {book.ISBN}");
        }
    }

    // -----------------------------------------
    // 2. Open/Closed Principle (OCP)
    // -----------------------------------------
    public interface IShape
    {
        double GetArea();
    }

    public class Circle : IShape
    {
        public double Radius { get; }
        public Circle(double radius) => Radius = radius;
        public double GetArea() => Math.PI * Radius * Radius;
    }

    public class Rectangle : IShape
    {
        public double Width { get; }
        public double Height { get; }
        public Rectangle(double w, double h) { Width = w; Height = h; }
        public double GetArea() => Width * Height;
    }

    public class Triangle : IShape
    {
        public double Base { get; }
        public double Height { get; }
        public Triangle(double b, double h) { Base = b; Height = h; }
        public double GetArea() => 0.5 * Base * Height;
    }

    
        public class AreaCalculator
        {
            public double TotalArea(IShape shape) => shape.GetArea();
        }
    

    // -----------------------------------------
    // 3. Liskov Substitution Principle (LSP)
    // -----------------------------------------
    public abstract class Bird
    {
        public abstract void Eat();
    }

    public abstract class FlyingBird : Bird
    {
        public abstract void Fly();
    }

    public class Sparrow : FlyingBird
    {
        public override void Eat() => Console.WriteLine("[LSP] Sparrow is eating.");
        public override void Fly() => Console.WriteLine("[LSP] Sparrow is flying.");
    }

    public class Ostrich : Bird
    {
        public override void Eat() => Console.WriteLine("[LSP] Ostrich is eating.");
        // No Fly() method — correct by design
    }

    // -----------------------------------------
    // 4. Interface Segregation Principle (ISP)
    // -----------------------------------------
    public interface IPrinter
    {
        void Print(string content);
    }

    public interface IScanner
    {
        void Scan(string document);
    }

    public interface IFax
    {
        void Fax(string document);
    }

    public class SimplePrinter : IPrinter
    {
        public void Print(string content) => Console.WriteLine($"[ISP] SimplePrinter: {content}");
    }

    public class MultiFunctionPrinter : IPrinter, IScanner
    {
        public void Print(string content) => Console.WriteLine($"[ISP] MFP Printing: {content}");
        public void Scan(string document) => Console.WriteLine($"[ISP] MFP Scanning: {document}");
    }

    public class FaxMachine : IFax
    {
        public void Fax(string document) => Console.WriteLine($"[ISP] Faxing: {document}");
    }

    // -----------------------------------------
    // 5. Dependency Inversion Principle (DIP)
    // -----------------------------------------
    public interface IPaymentService
    {
        void ProcessPayment(decimal amount);
    }

    public class PayPalService : IPaymentService
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"[DIP] PayPal processed payment of ${amount}.");
        }
    }

    public class StripeService : IPaymentService
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"[DIP] Stripe processed payment of ${amount}.");
        }
    }

    public class PaymentProcessor
    {
        private readonly IPaymentService _paymentService;

        public PaymentProcessor(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public void Process(decimal amount)
        {
            _paymentService.ProcessPayment(amount);
        }
    }

    // -----------------------------------------
    // Main Program – Demonstrates All Principles
    // -----------------------------------------
    class Design
    {
        static void Main()
        {
            Console.WriteLine("=== SOLID Principles Demo ===\n");

            // --- SRP Demo ---
            Console.WriteLine(">> SRP: Book");
            var book = new Books("Clean Architecture", "Robert C. Martin", "9780134494166");
            var bookRepo = new BookRepository();
            var bookPrinter = new BookPrinter();
            bookRepo.Save(book);
            bookPrinter.Print(book);

            // --- OCP Demo ---
            Console.WriteLine("\n>> OCP: Shapes");
          
            IShape circle = new Circle(4);
            var areaCalculator = new AreaCalculator();
            Console.WriteLine($"Total Area of Circle : {areaCalculator.TotalArea(circle):0.00}");
            IShape rect = new Rectangle(3,6);
            Console.WriteLine($"Total Area of Rectangle : {areaCalculator.TotalArea(rect):0.00}");
            IShape tri = new Triangle(5, 3);
            Console.WriteLine($"Total Area of Triangle : {areaCalculator.TotalArea(tri):0.00}");

            // --- LSP Demo ---
            Console.WriteLine("\n>> LSP: Birds");
            Bird sparrow = new Sparrow();
            sparrow.Eat();
            ((FlyingBird)sparrow).Fly();

            Bird ostrich = new Ostrich();
            ostrich.Eat();
            // ((FlyingBird)ostrich).Fly(); // Not allowed

            // --- ISP Demo ---
            Console.WriteLine("\n>> ISP: Devices");
            IPrinter printer = new SimplePrinter();
            printer.Print("Invoice");

            var mfp = new MultiFunctionPrinter();
            mfp.Print("Report");
            mfp.Scan("ID Document");

            var fax = new FaxMachine();
            fax.Fax("Agreement Form");

            // --- DIP Demo ---
            Console.WriteLine("\n>> DIP: Payment");
            IPaymentService paypal = new PayPalService();
            IPaymentService stripe = new StripeService();

            var processor1 = new PaymentProcessor(paypal);
            processor1.Process(150.00m);

            var processor2 = new PaymentProcessor(stripe);
            processor2.Process(200.00m);

            Console.WriteLine("\n=== End of Demo ===");
            Console.ReadLine();
        }
    }
}
