using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Member
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    class BorrowRecord
    {
        public Member Member { get; set; }
        public string BookName { get; set; }
        public DateTime BorrowDateTime { get; set; }
    }
    internal class LibraryBookBorrowingSystem
    {
        static void Main()
        {
            
            List<Member> members = new List<Member>
            {
                 new Member {Id =7 , Name ="Saptaparnee"},
                 new Member { Id = 1, Name = "Alice" },
                new Member { Id = 2, Name = "Bob" },
                new Member { Id = 3, Name = "Charlie" }
            };
            Console.WriteLine($" We have the following Members : ");
            foreach (var m in members)
            {
                Console.WriteLine($" Member Name : {m.Name} , Member Id : {m.Id}");
            }
                Console.WriteLine($"----------------------------------------------");
                HashSet<string> uniqueBooks = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Pride and Prejudice",
            "Emma",
            "Pursuasion",
            "Sense and Sensibility",
            "It Ends With Us"
        };
            Console.WriteLine($" The following are books available ");
            foreach (var s in uniqueBooks)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine($"----------------------------------------------");
            List<BorrowRecord> borrowRecord = new List<BorrowRecord>
            {
                new BorrowRecord { Member = members[0], BookName ="Pride and Prejudice", BorrowDateTime =DateTime.Now.AddDays(-7) },
                new BorrowRecord { Member = members[0], BookName ="Sense and Sensibility", BorrowDateTime =DateTime.Now.AddDays(-14) },
                new BorrowRecord { Member = members[0], BookName ="Persuasion", BorrowDateTime =DateTime.Now.AddDays(-21) },
                new BorrowRecord { Member = members[1], BookName ="Pride and Prejudice", BorrowDateTime =DateTime.Now.AddDays(-7) },
                new BorrowRecord { Member = members[1], BookName ="Emma", BorrowDateTime =DateTime.Now.AddDays(-10) },
                new BorrowRecord { Member = members[2], BookName ="It Ends With Us", BorrowDateTime =DateTime.Now.AddDays(-3) },
                new BorrowRecord { Member = members[2], BookName ="Sense and Sensibility", BorrowDateTime =DateTime.Now.AddDays(-7) },
                new BorrowRecord { Member = members[3], BookName ="Pride and Prejudice", BorrowDateTime =DateTime.Now.AddDays(-5) },
                new BorrowRecord { Member = members[3], BookName ="It Ends With Us", BorrowDateTime =DateTime.Now.AddDays(-9) },
                new BorrowRecord { Member = members[3], BookName ="Persuasion", BorrowDateTime =DateTime.Now.AddDays(-11) }
            };
            var groupByTitle = borrowRecord.GroupBy(br => br.BookName);
            Console.WriteLine("========Grouping By Book Title =========");
            foreach (var rec in groupByTitle)
            {
                Console.WriteLine($"Book Title : {rec.Key}");
                foreach (var m in rec)
                {
                    Console.WriteLine($"Member Name : {m.Member.Name} , Member Id : {m.Member.Id} , BorrowDate : {m.BorrowDateTime}");
                }
                Console.WriteLine("------------------------------------------");
            }
            Console.WriteLine($"Enter name of Member whose Records U need : ");
            string name = Console.ReadLine();
            if (members.Any(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) == false)
            {
                Console.WriteLine("Member doesnot exists");
            }
            else 
            {
                var groupByMember = borrowRecord.Where(m => m.Member.Name == name).Select(m => new { m.BookName, m.BorrowDateTime });
                Console.WriteLine($"------------------------ Records of Member: {name} :---------------");

                Console.WriteLine($"-------------Member Name : {name} has follwoing records of books borrowed :----------- ");
                foreach (var m in groupByMember)
                {
                    Console.WriteLine($" Book Name : {m.BookName} and boorrowed on {m.BorrowDateTime}");
                }

            }

            Console.WriteLine($"Enter name of Book whose Records U need : ");
            string book = Console.ReadLine();
            if (uniqueBooks.Contains(book) == false)
            {
                Console.WriteLine($"Invalid Book Name");
            }
            else
            {
                var groupByBook = borrowRecord.Where(b => b.BookName == book).Select(b => b.Member);
                Console.WriteLine($"------------------------ Records of Book : {book} :---------------");


                foreach (var m in groupByBook)
                {
                    Console.WriteLine($" Member Name : {m.Name} , Member Id : {m.Id} ");
                }
            }
            Console.ReadLine();
        } 
        }
    }

