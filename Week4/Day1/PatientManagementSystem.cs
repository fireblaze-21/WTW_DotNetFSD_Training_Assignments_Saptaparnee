using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp3
{
    class PatientManagementSystem
    {
        static void Main(string[] args)
        {
            PatientManager manager = new PatientManager("patients.txt");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Patient Management System");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. View All Patients");
                Console.WriteLine("3. Update Patient");
                Console.WriteLine("4. Delete Patient");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option (1-5): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        manager.AddPatient();
                        break;
                    case "2":
                        manager.ViewAllPatients();
                        break;
                    case "3":
                        manager.UpdatePatient();
                        break;
                    case "4":
                        manager.DeletePatient();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }

    class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Diagnosis { get; set; }

        public override string ToString()
        {
            return $"{ID},{Name},{Age},{Diagnosis}";
        }
    }

    class PatientManager
    {
        private List<Patient> patients = new List<Patient>();
        private readonly string filePath;

        public PatientManager(string path)
        {
            filePath = path;
            LoadPatientsFromFile();
        }

        private void LoadPatientsFromFile()
        {
            patients.Clear();

            try
            {
                if (!File.Exists(filePath))
                    File.Create(filePath).Close();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var parts = line.Split(',');
                        if (parts.Length == 4 &&
                           int.TryParse(parts[0], out int id) &&
                           int.TryParse(parts[2], out int age))
                        {
                            patients.Add(new Patient
                            {
                                ID = id,
                                Name = parts[1],
                                Age = age,
                                Diagnosis = parts[3]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading patient file: {ex.Message}");
                Console.ReadLine();
            }
        }
        private void SavePatientsToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false)) // false = overwrite file
                {
                    foreach (var patient in patients)
                    {
                        writer.WriteLine(patient.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
                Console.ReadLine();
            }
        }
        public void AddPatient()
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int id = int.Parse(Console.ReadLine());

                if (patients.Any(p => p.ID == id))
                {
                    Console.WriteLine("Patient ID already exists.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Age: ");
                int age = int.Parse(Console.ReadLine());
                Console.Write("Enter Diagnosis: ");
                string diagnosis = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(diagnosis))
                {
                    Console.WriteLine("Name and Diagnosis cannot be empty.");
                    Console.ReadLine();
                    return;
                }

                patients.Add(new Patient { ID = id, Name = name, Age = age, Diagnosis = diagnosis });
                SavePatientsToFile();
                Console.WriteLine("Patient added successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("ID and Age must be numeric.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            Console.ReadLine();
        }

        public void ViewAllPatients()
        {
            if (patients.Count == 0)
            {
                Console.WriteLine("No patient records found.");
            }
            else
            {
                Console.WriteLine("\nID\tName\t\tAge\tDiagnosis");
                Console.WriteLine("----------------------------------------------------");
                foreach (var p in patients)
                {
                    Console.WriteLine($"{p.ID}\t{p.Name}\t\t{p.Age}\t{p.Diagnosis}");
                }
            }
            Console.ReadLine();
        }

        public void UpdatePatient()
        {
            try
            {
                Console.Write("Enter Patient ID to update: ");
                int id = int.Parse(Console.ReadLine());

                var patient = patients.FirstOrDefault(p => p.ID == id);
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter new Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter new Age: ");
                int age = int.Parse(Console.ReadLine());
                Console.Write("Enter new Diagnosis: ");
                string diagnosis = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(diagnosis))
                {
                    Console.WriteLine("Name and Diagnosis cannot be empty.");
                    Console.ReadLine();
                    return;
                }

                patient.Name = name;
                patient.Age = age;
                patient.Diagnosis = diagnosis;

                SavePatientsToFile();
                Console.WriteLine("Patient updated successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. ID and Age must be numbers.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        public void DeletePatient()
        {
            try
            {
                Console.Write("Enter Patient ID to delete: ");
                int id = int.Parse(Console.ReadLine());

                var patient = patients.FirstOrDefault(p => p.ID == id);
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    Console.ReadLine();
                    return;
                }

                patients.Remove(patient);
                SavePatientsToFile();
                Console.WriteLine("Patient deleted successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid ID format.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}

