// ST10355049 PROG6221 POE Liam Knipe

using RecipeManagerWPF;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ST10355049_POEPart3_LiamKnipe
{
    public partial class ViewRecipeWindow : Window
    {
        private RecipeManager recipeManager;
        private ObservableCollection<string> stepsWithNumbers;

        // Constructor
        public ViewRecipeWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            this.recipeManager = recipeManager;
            stepsWithNumbers = new ObservableCollection<string>();

            PopulateRecipeListBox();
            PopulateFoodGroupFilterComboBox();
        }

        // ---------

        // Method to populate recipe list box
        private void PopulateRecipeListBox()
        {
            RecipeListBox.Items.Clear();
            foreach (Recipe recipe in recipeManager.Recipes.OrderBy(r => r.Name))
            {
                RecipeListBox.Items.Add(recipe);
            }
        }

        // ---------

        // Method to populate food group filter combo box
        private void PopulateFoodGroupFilterComboBox()
        {
            // Assuming you have a method in RecipeManager to get unique food groups
            var foodGroups = recipeManager.GetUniqueFoodGroups();
            foreach (var group in foodGroups)
            {
                FoodGroupFilterComboBox.Items.Add(group);
            }
        }

        // ---------

        // Event handler for applying filters
        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            var filteredRecipes = recipeManager.Recipes.AsEnumerable();

            // Filter by Ingredient
            if (!string.IsNullOrWhiteSpace(IngredientFilterTextBox.Text))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.IndexOf(IngredientFilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            // Filter by Food Group
            if (FoodGroupFilterComboBox.SelectedItem != null)
            {
                string selectedFoodGroup = FoodGroupFilterComboBox.SelectedItem.ToString();
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup == selectedFoodGroup));
            }

            // Filter by Max Calories
            if (int.TryParse(MaxCaloriesTextBox.Text, out int maxCalories))
            {
                filteredRecipes = filteredRecipes.Where(r => r.CalculateTotalCalories() <= maxCalories);
            }

            RecipeListBox.Items.Clear();
            foreach (Recipe recipe in filteredRecipes.OrderBy(r => r.Name))
            {
                RecipeListBox.Items.Add(recipe);
            }
        }

        // ---------

        // Event handler for clearing filters
        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            IngredientFilterTextBox.Clear();
            FoodGroupFilterComboBox.SelectedItem = null;
            MaxCaloriesTextBox.Clear();
            PopulateRecipeListBox();
        }

        // ---------

        // Event handler for recipe selection changed
        private void RecipeListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Recipe selectedRecipe = (Recipe)RecipeListBox.SelectedItem;

            if (selectedRecipe != null)
            {
                RecipeNameTextBlock.Text = selectedRecipe.Name;
                IngredientsDataGrid.ItemsSource = selectedRecipe.Ingredients;
                stepsWithNumbers.Clear();
                int stepNumber = 1;
                foreach (string step in selectedRecipe.Steps)
                {
                    stepsWithNumbers.Add($"{stepNumber++}. {step}");
                }
                StepsListBox.ItemsSource = stepsWithNumbers;
                TotalCaloriesRun.Text = selectedRecipe.CalculateTotalCalories().ToString();
                WarningTextBlock.Visibility = selectedRecipe.CalculateTotalCalories() > 300 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        // ---------

        // Event handler for scaling recipe
        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            ScaleRecipeWindow scaleRecipeWindow = new ScaleRecipeWindow(recipeManager);
            scaleRecipeWindow.Show();
            this.Close(); // Optionally close the ViewRecipeWindow
        }

        // ---------

        // Event handler for returning to menu
        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Use the GetInstance method to get the existing instance of the MenuWindow
            MenuWindow menuWindow = MenuWindow.GetInstance(recipeManager);
            menuWindow.Show(); // Show the MenuWindow
            this.Close(); // Close the current ViewRecipeWindow
        }
    }
}

// ----- End of ViewRecipeWindow.xaml.cs -----