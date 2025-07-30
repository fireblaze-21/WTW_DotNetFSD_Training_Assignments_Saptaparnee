using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    // Data models
    public class EmployeeProfile
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public string Department { get; set; } = "";
    }

    public class PayrollDetails
    {
        public decimal Salary { get; set; }
        public DateTime LastPayDate { get; set; }
    }

    public class TrainingRecords
    {
        public List<string> CompletedCourses { get; set; } = new List<string>();
    }

    public class EmployeeReport
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; } = "";
        public string Department { get; set; } = "";
        public decimal Salary { get; set; }
        public List<string> CompletedCourses { get; set; } = new List<string>();
        public DateTime ReportGenerationDate { get; set; }
    }

    public static class Logger
    {
        public static void Log(string message) => Console.WriteLine(message);
    }

    public class EmployeeDataService
    {
        // Simulate HR database query
        public async Task<EmployeeProfile> GetEmployeeProfileAsync(int id)
        {
            await Task.Delay(1000); // Simulated latency
            if (id <= 0)
                throw new ArgumentException("Invalid employee ID");

            // Simulated data
            return new EmployeeProfile { ID = id, Name = $"Employee_{id}", Department = "Engineering" };
        }

        // Simulate Payroll system API
        public async Task<PayrollDetails> GetPayrollDetailsAsync(int id)
        {
            await Task.Delay(800);
            if (id <= 0)
                throw new ArgumentException("Invalid employee ID");

            // Simulated data
            return new PayrollDetails { Salary = 75000m, LastPayDate = DateTime.Now.AddDays(-15) };
        }

        // Simulate Training system API
        public async Task<TrainingRecords> GetTrainingRecordsAsync(int id)
        {
            await Task.Delay(1200);
            if (id <= 0)
                throw new ArgumentException("Invalid employee ID");

            // Simulated data
            return new TrainingRecords { CompletedCourses = new List<string> { "C# Basics", "Async Programming" } };
        }
    }

    public class EmployeeReportGenerator
    {
        private readonly EmployeeDataService _dataService;

        public EmployeeReportGenerator(EmployeeDataService dataService)
        {
            _dataService = dataService;
        }
        public async Task<EmployeeReport> GenerateReportAsync(int employeeId)
        {
            try
            {
                var profileTask = _dataService.GetEmployeeProfileAsync(employeeId);
                var payrollTask = _dataService.GetPayrollDetailsAsync(employeeId);
                var trainingTask = _dataService.GetTrainingRecordsAsync(employeeId);

                await Task.WhenAll(profileTask, payrollTask, trainingTask);

                var profile = await profileTask;
                var payroll = await payrollTask;
                var training = await trainingTask;

                return await Task.Run(() =>
                {
                    return new EmployeeReport
                    {
                        EmployeeID = profile.ID,
                        Name = profile.Name,
                        Department = profile.Department,
                        Salary = payroll.Salary,
                        CompletedCourses = training.CompletedCourses,
                        ReportGenerationDate = DateTime.Now
                    };
                });
            }
            catch (AggregateException aggEx)
            {
                foreach (var ex in aggEx.InnerExceptions)
                    Logger.Log("Error: " + ex.Message);
                return null; // explicitly check for null at call site
            }
            catch (ArgumentException argEx)
            {
                Logger.Log("Argument Error: " + argEx.Message);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Log("Unexpected error: " + ex.Message);
                return null;
            }
        }
       
        public async Task<bool> SaveReportAsync(EmployeeReport report)
        {
            string folder = "Reports";
            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string filePath = Path.Combine(folder, $"employee_report_{report.EmployeeID}.txt");

                var sb = new StringBuilder();
                sb.AppendLine($"Report for {report.Name} (ID: {report.EmployeeID})");
                sb.AppendLine($"Department: {report.Department}");
                sb.AppendLine($"Salary: {report.Salary:C}");
                sb.AppendLine($"Courses: {string.Join(", ", report.CompletedCourses)}");
                sb.AppendLine($"Generated: {report.ReportGenerationDate:yyyy-MM-dd HH:mm:ss}");

                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    await writer.WriteAsync(sb.ToString());
                    await writer.FlushAsync();
                }
                return true;
            }
            catch (IOException ioEx)
            {
                Logger.Log($"File IO error while saving report: {ioEx.Message}");
                return false;
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Logger.Log($"Access denied while saving report: {uaEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log($"Unexpected error while saving report: {ex.Message}");
                return false;
            }
        }
    }

    class EmployeeManagementSystem
    {
        static async Task Main()
        {
            var dataService = new EmployeeDataService();
            var reportGenerator = new EmployeeReportGenerator(dataService);

            Console.Write("Enter Employee ID to generate report: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Invalid input for Employee ID.");
                return;
            }

            var report = await reportGenerator.GenerateReportAsync(employeeId);

            if (report == null)
            {
                Console.WriteLine("Failed to generate report for invalid ID.");
                return;
            }

            bool saved = await reportGenerator.SaveReportAsync(report);

            if (saved)
            {
                Console.WriteLine($"Report saved for employee ID {report.EmployeeID}");
                Console.WriteLine($"Generated Report: {report.Name}, {report.Department}, {report.Salary}, Courses: {string.Join(", ", report.CompletedCourses)}");
            }
            else
            {
                Console.WriteLine("Failed to save the report.");
            }
        }
    }
}

