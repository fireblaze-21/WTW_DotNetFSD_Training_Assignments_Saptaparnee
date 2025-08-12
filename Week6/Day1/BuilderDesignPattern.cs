
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp3
{
    // Product
    public class Meal
    {
        public string MainCourse { get; set; }
        public string Side { get; set; }
        public string Drink { get; set; }
        public string Dessert { get; set; }

        public void Display()
        {
            Console.WriteLine("Meal Components:");
            Console.WriteLine($"  Main Course: {MainCourse ?? "None"}");
            Console.WriteLine($"  Side       : {Side ?? "None"}");
            Console.WriteLine($"  Drink      : {Drink ?? "None"}");
            Console.WriteLine($"  Dessert    : {Dessert ?? "None"}");
            Console.WriteLine();
        }
    }

    // Builder Interface
    public interface IMealBuilder
    {
        IMealBuilder SetMainCourse(string main);
        IMealBuilder SetSide(string side);
        IMealBuilder SetDrink(string drink);
        IMealBuilder SetDessert(string dessert);
        Meal Build();
    }

    // Concrete Builder
    public class MealBuilder : IMealBuilder
    {
        private Meal _meal = new Meal();

        public IMealBuilder SetMainCourse(string main)
        {
            _meal.MainCourse = main;
            return this;
        }

        public IMealBuilder SetSide(string side)
        {
            _meal.Side = side;
            return this;
        }

        public IMealBuilder SetDrink(string drink)
        {
            _meal.Drink = drink;
            return this;
        }

        public IMealBuilder SetDessert(string dessert)
        {
            _meal.Dessert = dessert;
            return this;
        }

        public Meal Build()
        {
            Meal builtMeal = _meal;
            _meal = new Meal(); // Reset for next build
            return builtMeal;
        }
    }

    // Director
    public class MealDirector
    {
        public Meal ConstructValueMeal(IMealBuilder builder)
        {
            return builder
                .SetMainCourse("Burger")
                .SetSide("Fries")
                .SetDrink("Soda")
                .Build();
        }

        public Meal ConstructHealthyMeal(IMealBuilder builder)
        {
            return builder
                .SetMainCourse("Grilled Chicken")
                .SetSide("Salad")
                .SetDrink("Water")
                .SetDessert("Ice Cream")
                .Build();
        }

        // More predefined meals like KidsMeal, VeganMeal can be added here
    }

    // Client
    class BuilderDesignPattern
    {
        static void Main()
        {
            IMealBuilder builder = new MealBuilder();
            MealDirector director = new MealDirector();

            // Value Meal
            Meal valueMeal = director.ConstructValueMeal(builder);
            Console.WriteLine("Value Meal:");
            valueMeal.Display();

            // Healthy Meal
            Meal healthyMeal = director.ConstructHealthyMeal(builder);
            Console.WriteLine("Healthy Meal:");
            healthyMeal.Display();

            // Custom Meal
            Meal customMeal = builder
                .SetMainCourse("Pizza")
                .SetDrink("Juice")
                .Build();

            Console.WriteLine("Custom Meal:");
            customMeal.Display();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
