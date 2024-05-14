namespace RecipeApp
{
    // This class represents an ingredient in a recipe
    class Ingredient
    {
        // Properties for ingredient details
        public string Name { get; }
        public double Quantity { get; set; }
        public string Unit { get; }
        public int Calories { get; }
        public string FoodGroup { get; }

        // Constructor initializes the ingredient properties
        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}
