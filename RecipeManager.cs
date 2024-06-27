// ST10355049 PROG6221 POE Liam Knipe

using ST10355049_POEPart3_LiamKnipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RecipeManagerWPF
{
    public class RecipeManager
    {
        private ObservableCollection<Recipe> recipes;

        public ObservableCollection<Recipe> Recipes
        {
            get { return recipes; }
        }

        public event EventHandler<RecipeEventArgs> OnRecipeAdded;
        public event EventHandler OnRecipeCleared;

        // Constructor
        public RecipeManager()
        {
            recipes = new ObservableCollection<Recipe>();
        }

        // ---------

        // Method to add a recipe
        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            OnRecipeAdded?.Invoke(this, new RecipeEventArgs(recipe));
        }

        // ---------

        // Method to clear a recipe
        public void ClearRecipe(Recipe recipe)
        {
            recipes.Remove(recipe);
            OnRecipeCleared?.Invoke(this, EventArgs.Empty);
        }

        // ---------

        // Method to get unique food groups
        public IEnumerable<string> GetUniqueFoodGroups()
        {
            // Use HashSet to avoid duplicates
            var foodGroups = new HashSet<string>();

            foreach (var recipe in Recipes)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    foodGroups.Add(ingredient.FoodGroup);
                }
            }

            return foodGroups.OrderBy(fg => fg); // Return the food groups sorted alphabetically
        }

        // ---------

        // Method to get a recipe by index
        public Recipe GetRecipe(int index)
        {
            if (index >= 0 && index < recipes.Count)
            {
                return recipes[index];
            }
            else
            {
                return null;
            }
        }

        // ---------

        // Method to get a recipe by name
        public Recipe GetRecipeByName(string name)
        {
            foreach (var recipe in Recipes)
            {
                if (recipe.Name == name)
                {
                    return recipe;
                }
            }

            // If no matching recipe is found, throw an exception
            throw new ArgumentException("No recipe found with the given name.");
        }

        // ---------

        // Method to filter recipes
        public List<Recipe> FilterRecipes(string ingredientName, string foodGroup, int maxCalories)
        {
            return Recipes.Where(recipe =>
                (string.IsNullOrEmpty(ingredientName) || recipe.Ingredients.Any(i => i.Name == ingredientName)) &&
                (string.IsNullOrEmpty(foodGroup) || recipe.Ingredients.Any(i => i.FoodGroup == foodGroup)) &&
                recipe.TotalCalories <= maxCalories
            ).ToList();
        }
    }

    public class RecipeEventArgs : EventArgs
    {
        public Recipe Recipe { get; }

        public RecipeEventArgs(Recipe recipe)
        {
            Recipe = recipe;
        }
    }
}

// ----- End of RecipeManager.cs -----