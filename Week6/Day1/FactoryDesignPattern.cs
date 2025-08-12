using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp3
    {
        // 1. Common Interface
        public interface IShipment
        {
            double CalculateCost(double weight, double distance);
        }

        // 2. Concrete Shipment Classes

        public class GroundShipment : IShipment
        {
            public double CalculateCost(double weight, double distance)
            {
                // Formula: weight * 0.5 + distance * 0.1
                return weight * 0.5 + distance * 0.1;
            }
        }

        public class AirShipment : IShipment
        {
            public double CalculateCost(double weight, double distance)
            {
                // Formula: weight * 2.0 + distance * 0.5 + 50
                return weight * 2.0 + distance * 0.5 + 50;
            }
        }

        public class SeaShipment : IShipment
        {
            public double CalculateCost(double weight, double distance)
            {
                // Formula: weight * 0.3 + distance * 0.2 + 100
                return weight * 0.3 + distance * 0.2 + 100;
            }
        }

        // 3. Shipment Factory
        public static class ShipmentFactory
        {
            public static IShipment GetShipment(string shipmentType)
            {
                if (string.IsNullOrWhiteSpace(shipmentType))
                    throw new ArgumentNullException(nameof(shipmentType), "Shipment type cannot be null or empty.");

                switch (shipmentType.Trim().ToLower())
                {
                    case "ground":
                        return new GroundShipment();

                    case "air":
                        return new AirShipment();

                    case "sea":
                        return new SeaShipment();

                    default:
                        throw new ArgumentException($"Invalid shipment type: {shipmentType}");
                }
            }
        }

        // 4. Client Code
        public class FactoryDesignPattern
        {
            public static void Main(string[] args)
            {
                // Example values
                double weight = 10;     // in kg
                double distance = 100;  // in km

                string[] shipmentTypes = { "ground", "air", "sea", "space" }; // "space" is invalid to test error handling

                foreach (var type in shipmentTypes)
                {
                    try
                    {
                        IShipment shipment = ShipmentFactory.GetShipment(type);
                        double cost = shipment.CalculateCost(weight, distance);
                        Console.WriteLine($"{type.ToUpper()} shipment cost: ${cost:F2}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating shipment for type '{type}': {ex.Message}");
                    }
                }

                Console.ReadLine();
            }
        }
    }

