using System;
using System.Collections.Generic;

namespace RecipeApp
{
    // Class representing a recipe
    class Recipe
    {
        // Properties for the recipe name and lists for ingredients and steps
        public string Name { get; set; }
        private List<Ingredient> ingredients;
        private List<Step> steps;
        private Dictionary<Ingredient, double> originalQuantities;

        // Constructor initializes lists and the dictionary for original quantities
        public Recipe()
        {
            ingredients = new List<Ingredient>();
            steps = new List<Step>();
            originalQuantities = new Dictionary<Ingredient, double>();
        }

        // Method to add an ingredient to the recipe
        public void AddIngredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            var ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
            ingredients.Add(ingredient);
            originalQuantities[ingredient] = quantity;
        }

        // Method to add a step to the recipe
        public void AddStep(string stepDescription)
        {
            steps.Add(new Step(stepDescription));
        }

        // Method to get the list of ingredients
        public List<Ingredient> GetIngredients()
        {
            return ingredients;
        }

        // Method to calculate the total calories of the recipe
        public int CalculateTotalCalories()
        {
            int totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }

        // Method to display the recipe details
        public void DisplayRecipe(RecipeApp.CalorieNotificationHandler notificationHandler)
        {
            Console.WriteLine($"\nRecipe: {Name}");
            Console.WriteLine("Ingredients:");
            int totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}, {ingredient.Calories} calories, Food Group: {ingredient.FoodGroup}");
                totalCalories += ingredient.Calories;
            }
            Console.WriteLine($"\nTotal Calories: {totalCalories}");

            // Check if total calories exceed 300 and invoke the notification handler if true
            if (totalCalories > 300)
            {
                notificationHandler?.Invoke("The total calories exceed 300!");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i].Description}");
            }
        }

        // Method to scale the recipe ingredients by a factor
        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity = originalQuantities[ingredient] * factor;
            }
        }
    }
}
