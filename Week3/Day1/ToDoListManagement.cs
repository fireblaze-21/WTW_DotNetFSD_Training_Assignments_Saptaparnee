using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class ToDoListManagement
    {
        static void DisplayMenu()
        {
            Console.WriteLine("To-Do List Manager");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Remove Task");
            Console.WriteLine("4. Exit");
        }

        static void AddTask(List<string> tasks)
        {
            Console.Write("Enter task: ");
            string task = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(task))
            {
                Console.WriteLine("Task description cannot be empty.");
                return;
            }

            tasks.Add(task);
            Console.WriteLine("Task added!");
        }

        static void ViewTasks(List<string> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks in the list.");
                return;
            }

            Console.WriteLine("Tasks:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
        }

        static void RemoveTask(List<string> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks to remove.");
                return;
            }

            Console.Write("Enter task number to remove: ");
            string input = Console.ReadLine()?.Trim();

            if (!int.TryParse(input, out int taskNumber))
            {
                Console.WriteLine("Invalid input. Please enter a numeric task number.");
                return;
            }

            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                Console.WriteLine("Invalid task number.");
                return;
            }

            string removedTask = tasks[taskNumber - 1];
            tasks.RemoveAt(taskNumber - 1);
            Console.WriteLine($"Removed: {removedTask}");
        }

        static void Main()
        {
            List<string> tasks = new List<string>();
            bool running = true;

            while (running)
            {
                DisplayMenu();
                Console.Write("Choose an option: ");
                string input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        AddTask(tasks);
                        break;
                    case "2":
                        ViewTasks(tasks);
                        break;
                    case "3":
                        RemoveTask(tasks);
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
