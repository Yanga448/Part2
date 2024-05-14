using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    // This class manages the Recipe Application
    class RecipeApp
    {
        // A list to store multiple recipes
        private List<Recipe> recipes;

        // Delegate for calorie notification
        public delegate void CalorieNotificationHandler(string message);

        // Event for calorie notification
        public event CalorieNotificationHandler CalorieNotification;

        // Constructor initializes the recipe list and subscribes to the notification event
        public RecipeApp()
        {
            recipes = new List<Recipe>();
            CalorieNotification += DisplayNotification;
        }

        // This method starts the Recipe Application
        public void StartApp()
        {
            Console.WriteLine("Welcome to the Enhanced Recipe App!");

            while (true)
            {
                // Display menu options to the user
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. View all recipes");
                Console.WriteLine("3. Display a specific recipe");
                Console.WriteLine("4. Scale a recipe");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                // Handle the user's menu choice
                switch (Console.ReadLine())
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        DisplayAllRecipes();
                        break;
                    case "3":
                        DisplaySpecificRecipe();
                        break;
                    case "4":
                        ScaleRecipe();
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using the Recipe App!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // This method allows the user to add a new recipe
        private void AddRecipe()
        {
            Recipe recipe = new Recipe();

            // Get the name of the recipe from the user
            Console.Write("\nEnter the name of the recipe: ");
            recipe.Name = Console.ReadLine();

            // Get the number of ingredients from the user
            Console.WriteLine("\nEnter the number of ingredients:");
            if (!int.TryParse(Console.ReadLine(), out int numOfIngredients) || numOfIngredients <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive whole number.");
                return;
            }

            // Loop to add each ingredient
            for (int i = 0; i < numOfIngredients; i++)
            {
                Console.WriteLine($"\nEnter details for ingredient {i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Quantity: ");
                if (!double.TryParse(Console.ReadLine(), out double quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a positive number.");
                    i--;
                    continue;
                }
                Console.Write("Unit of Measurement: ");
                string unit = Console.ReadLine();
                Console.Write("Calories: ");
                if (!int.TryParse(Console.ReadLine(), out int calories) || calories < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative number.");
                    i--;
                    continue;
                }
                Console.Write("Food Group: ");
                string foodGroup = Console.ReadLine();

                // Add the ingredient to the recipe
                recipe.AddIngredient(name, quantity, unit, calories, foodGroup);
            }

            // Get the number of steps from the user
            Console.WriteLine("\nEnter the number of steps:");
            if (!int.TryParse(Console.ReadLine(), out int numOfSteps) || numOfSteps <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive whole number.");
                return;
            }

            // Loop to add each step
            for (int i = 0; i < numOfSteps; i++)
            {
                Console.WriteLine($"\nEnter step {i + 1}:");
                string step = Console.ReadLine();
                recipe.AddStep(step);
            }

            // Add the complete recipe to the list of recipes
            recipes.Add(recipe);
            Console.WriteLine("\nRecipe successfully added!");
        }

        // This method displays all recipes in alphabetical order
        private void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                return;
            }

            // Sort recipes by name in alphabetical order and display them
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
            Console.WriteLine("\nRecipes in alphabetical order:");
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipes[i].Name}");
            }
        }

        // This method allows the user to display a specific recipe
        private void DisplaySpecificRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                return;
            }

            DisplayAllRecipes();
            Console.WriteLine("\nEnter the number of the recipe you want to display:");
            if (!int.TryParse(Console.ReadLine(), out int recipeIndex) || recipeIndex <= 0 || recipeIndex > recipes.Count)
            {
                Console.WriteLine("\nInvalid input. Returning to main menu.");
                return;
            }

            // Display the selected recipe
            recipes[recipeIndex - 1].DisplayRecipe(CalorieNotification);
        }

        // This method allows the user to scale a specific recipe
        private void ScaleRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                return;
            }

            DisplayAllRecipes();
            Console.WriteLine("\nEnter the number of the recipe you want to scale:");
            if (!int.TryParse(Console.ReadLine(), out int recipeIndex) || recipeIndex <= 0 || recipeIndex > recipes.Count)
            {
                Console.WriteLine("\nInvalid input. Returning to main menu.");
                return;
            }

            Console.WriteLine("\nEnter the scaling factor (0.5 for half, 2 for double, 3 for triple):");
            if (!double.TryParse(Console.ReadLine(), out double factor) || factor <= 0)
            {
                Console.WriteLine("\nInvalid input. Returning to main menu.");
                return;
            }

            // Scale the selected recipe
            recipes[recipeIndex - 1].ScaleRecipe(factor);
            Console.WriteLine("\nRecipe scaled successfully!");
            recipes[recipeIndex - 1].DisplayRecipe(CalorieNotification);
        }

        // This method displays a notification message
        private void DisplayNotification(string message)
        {
            Console.WriteLine($"\nNotification: {message}");
        }
    }
}
