using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp3
{
    // Singleton Configuration Manager
    public sealed class ConfigurationManager
    {
        private static ConfigurationManager _instance = null;
        private static readonly object _lock = new object();
        private readonly Dictionary<string, string> _settings;

        // Private constructor to prevent external instantiation
        private ConfigurationManager()
        {
            _settings = new Dictionary<string, string>();
            LoadSettings(); // Load only once
        }

        // Public static property for global access
        public static ConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock) // First check (before lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigurationManager(); // Create instance
                        }
                    }
                }
                return _instance;
            }
        }

        // Simulate loading settings from a config source
        private void LoadSettings()
        {
            _settings["DatabaseConnection"] = "Server=localhost;Database=test;";
            _settings["ApiKey"] = "12345-abcde";
            // Add more settings as needed
        }

        // Retrieve a setting by key
        public string GetSetting(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Setting key cannot be null.");

            if (_settings.TryGetValue(key, out string value))
            {
                return value;
            }

            throw new KeyNotFoundException($"Configuration key '{key}' not found.");
        }

        // Optional: allow setting updates in a thread-safe way
        public void SetSetting(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Setting key cannot be null.");

            lock (_lock)
            {
                _settings[key] = value;
            }
        }
    }

    // Client code demonstrating usage
    class SingletonDesignPattern
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Main Thread: Accessing ConfigurationManager...");

                var config1 = ConfigurationManager.Instance;
                string dbConnection = config1.GetSetting("DatabaseConnection");
                Console.WriteLine($"Main Thread: DatabaseConnection = {dbConnection}");

                // Start another thread to access the same singleton
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine("Worker Thread: Accessing ConfigurationManager...");

                    var config2 = ConfigurationManager.Instance;
                    string apiKey = config2.GetSetting("ApiKey");
                    Console.WriteLine($"Worker Thread: ApiKey = {apiKey}");

                    // Verify same instance
                    Console.WriteLine($"Same Instance in Thread? {object.ReferenceEquals(config1, config2)}");
                });

                thread.Start();
                thread.Join(); // Wait for thread to complete

                // Try accessing a non-existent key
                try
                {
                    string invalid = config1.GetSetting("InvalidKey");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled Exception: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
