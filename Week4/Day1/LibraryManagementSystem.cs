using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp3
{
    class LibraryManagementSystem
    {
        static void Main(string[] args)
        {
            BookManager manager = new BookManager("books.txt");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Library Management System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option (1-5): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": manager.AddBook(); break;
                    case "2": manager.ViewAllBooks(); break;
                    case "3": manager.UpdateBook(); break;
                    case "4": manager.DeleteBook(); break;
                    case "5": return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }

    class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }

        public override string ToString()
        {
            return $"{ID},{Title},{Author},{PublicationYear}";
        }
    }

    class BookManager
    {
        private List<Book> books = new List<Book>();
        private readonly string filePath;

        public BookManager(string path)
        {
            filePath = path;
            LoadBooksFromFile();
        }

        private void LoadBooksFromFile()
        {
            books.Clear();
            try
            {
                if (!File.Exists(filePath))
                    File.Create(filePath).Close();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 4 &&
                            int.TryParse(parts[0], out int id) &&
                            int.TryParse(parts[3], out int year))
                        {
                            books.Add(new Book
                            {
                                ID = id,
                                Title = parts[1],
                                Author = parts[2],
                                PublicationYear = year
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                Console.ReadLine();
            }
        }

        private void SaveBooksToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (var book in books)
                    {
                        writer.WriteLine(book.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
                Console.ReadLine();
            }
        }

        public void AddBook()
        {
            try
            {
                Console.Write("Enter Book ID: ");
                int id = int.Parse(Console.ReadLine());

                if (books.Any(b => b.ID == id))
                {
                    Console.WriteLine("Book with this ID already exists.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter Author: ");
                string author = Console.ReadLine();

                Console.Write("Enter Publication Year: ");
                int year = int.Parse(Console.ReadLine());

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Title and Author cannot be empty.");
                    Console.ReadLine();
                    return;
                }

                books.Add(new Book { ID = id, Title = title, Author = author, PublicationYear = year });
                SaveBooksToFile();
                Console.WriteLine("Book added successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. ID and Year must be numbers.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            Console.ReadLine();
        }

        public void ViewAllBooks()
        {
            Console.WriteLine("\nID\tTitle\t\t\tAuthor\t\t\tYear");
            Console.WriteLine("--------------------------------------------------------------");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.ID}\t{book.Title.PadRight(20)}\t{book.Author.PadRight(20)}\t{book.PublicationYear}");
            }

            if (books.Count == 0)
                Console.WriteLine("No books available.");

            Console.ReadLine();
        }

        public void UpdateBook()
        {
            try
            {
                Console.Write("Enter Book ID to update: ");
                int id = int.Parse(Console.ReadLine());

                var book = books.FirstOrDefault(b => b.ID == id);
                if (book == null)
                {
                    Console.WriteLine("Book not found.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter new Title: ");
                string title = Console.ReadLine();
                Console.Write("Enter new Author: ");
                string author = Console.ReadLine();
                Console.Write("Enter new Publication Year: ");
                int year = int.Parse(Console.ReadLine());

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Title and Author cannot be empty.");
                    Console.ReadLine();
                    return;
                }

                book.Title = title;
                book.Author = author;
                book.PublicationYear = year;

                SaveBooksToFile();
                Console.WriteLine("Book updated successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. ID and Year must be numbers.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        public void DeleteBook()
        {
            try
            {
                Console.Write("Enter Book ID to delete: ");
                int id = int.Parse(Console.ReadLine());

                var book = books.FirstOrDefault(b => b.ID == id);
                if (book == null)
                {
                    Console.WriteLine("Book not found.");
                    Console.ReadLine();
                    return;
                }

                books.Remove(book);
                SaveBooksToFile();
                Console.WriteLine("Book deleted successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. ID must be numeric.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}