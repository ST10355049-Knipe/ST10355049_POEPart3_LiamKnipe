// ST10355049 PROG6221 POE Liam Knipe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ST10355049_POEPart3_LiamKnipe
{
    public class Recipe
    {
        // Properties
        public string Name { get; set; }
        public ObservableCollection<string> Steps { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        // Constructor
        public Recipe(string name, ObservableCollection<Ingredient> ingredients, ObservableCollection<string> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
        }

        // ---------

        // Method to add an ingredient
        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        // ---------

        // Method to add a step
        public void AddStep(string step)
        {
            Steps.Add(step);
        }

        // ---------

        // Method to calculate total calories
        public int CalculateTotalCalories()
        {
            int totalCalories = 0;

            foreach (var ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories;
            }

            return totalCalories;
        }

        // ---------

        // Property to get total calories
        public int TotalCalories
        {
            get
            {
                return Ingredients.Sum(i => i.Calories);
            }
        }

        // ---------

        // Method to scale the recipe
        public void Scale(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                // Scale the quantity and calories of the ingredient by the scaling factor
                ingredient.Quantity *= factor;
                ingredient.Calories = (int)Math.Round(ingredient.Calories * factor); // Round the calories to the nearest integer

                // Conversion logic
                if (ingredient.UnitOfMeasurement == "gram" && ingredient.Quantity >= 1000)
                {
                    ingredient.Quantity /= 1000;
                    ingredient.UnitOfMeasurement = "kilogram";
                }
                else if (ingredient.UnitOfMeasurement == "milliliter" && ingredient.Quantity >= 1000)
                {
                    ingredient.Quantity /= 1000;
                    ingredient.UnitOfMeasurement = "liter";
                }
                else if (ingredient.UnitOfMeasurement == "teaspoon" && ingredient.Quantity >= 3)
                {
                    ingredient.Quantity /= 3;
                    ingredient.UnitOfMeasurement = "tablespoon";
                }
                // Add conversion from tablespoons to cups
                if (ingredient.UnitOfMeasurement == "tablespoon" && ingredient.Quantity >= 16)
                {
                    ingredient.Quantity /= 16;
                    ingredient.UnitOfMeasurement = "cup";
                }
            }
        }

        // ---------

        // Override ToString method for better representation
        public override string ToString()
        {
            // Calculate the total number of ingredients and steps
            int ingredientCount = Ingredients.Count;
            int stepCount = Steps.Count;

            // Use the TotalCalories property to get the total calories
            int totalCalories = TotalCalories;

            // Construct a string that includes the recipe name, ingredient count, step count, and total calories
            return $"{Name} - Ingredients: {ingredientCount}, Steps: {stepCount}, Total Calories: {totalCalories}";
        }

        // ---------

        // Method to reset the scale of the recipe
        public void ResetScale()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
                ingredient.UnitOfMeasurement = ingredient.OriginalUnitOfMeasurement;
                ingredient.Calories = ingredient.OriginalCalories; // Reset the calories to original
            }
        }
    }
}

// ----- End of Recipe.cs -----