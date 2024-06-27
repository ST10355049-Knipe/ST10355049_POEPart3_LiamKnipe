// ST10355049 PROG6221 POE Liam Knipe

using RecipeManagerWPF;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ST10355049_POEPart3_LiamKnipe
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        // Observable collections to store ingredients and steps
        private ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
        private ObservableCollection<string> steps = new ObservableCollection<string>();
        private RecipeManager recipeManager;
        private MenuWindow menuWindow;

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Constructor
        public AddRecipeWindow(RecipeManager recipeManager, MenuWindow menuWindow)
        {
            InitializeComponent();

            this.recipeManager = recipeManager;
            this.IngredientsDataGrid.ItemsSource = this.ingredients;
            this.menuWindow = menuWindow;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Event handler for adding an ingredient
        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(IngredientNameTextBox.Text) ||
                !double.TryParse(QuantityTextBox.Text, out double quantity) ||
                UnitComboBox.SelectedItem == null || // Check if a unit is selected
                !int.TryParse(CaloriesTextBox.Text, out int calories) ||
                FoodGroupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please enter valid values for all fields.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Get the selected unit from the ComboBox
            string unit = (UnitComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            // Get the selected food group from the ComboBox
            string foodGroup = FoodGroupComboBox.SelectedItem != null ? (FoodGroupComboBox.SelectedItem as ComboBoxItem).Content.ToString() : string.Empty;

            // Create a new ingredient
            Ingredient ingredient = new Ingredient(
                IngredientNameTextBox.Text,
                quantity,
                unit,
                calories,
                foodGroup
            );

            // Add the ingredient to the ingredients collection
            ingredients.Add(ingredient);

            // Update the IngredientsDataGrid
            IngredientsDataGrid.ItemsSource = null;
            IngredientsDataGrid.ItemsSource = ingredients;

            // Calculate the total calories
            int totalCalories = ingredients.Sum(i => i.Calories);

            // Update the TotalCaloriesLabel
            TotalCaloriesLabel.Content = "Total Calories: " + totalCalories;

            // Show a warning message if the total calories exceed 300
            if (totalCalories > 300)
            {
                MessageBox.Show("Warning: The total calories of the recipe exceed 300.", "High Calorie Count", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Ask the user if they want to add another ingredient
            MessageBoxResult result = MessageBox.Show("Do you want to add another ingredient?", "Add Ingredient", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // Clear the input fields for the next ingredient
                IngredientNameTextBox.Clear();
                QuantityTextBox.Clear();
                UnitComboBox.SelectedItem = null;
                CaloriesTextBox.Clear();
                FoodGroupComboBox.SelectedItem = null;
            }
            else
            {
                // Disable the AddIngredientButton
                AddIngredientButton.IsEnabled = false;

                // Show a message box asking the user to enter the steps
                MessageBox.Show("Please enter the steps.", "Add Steps", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Event handler for adding a step
        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the step description from the user
            string stepDescription = StepTextBox.Text;

            // Check if the step description is not empty
            if (string.IsNullOrWhiteSpace(stepDescription))
            {
                MessageBox.Show("Please enter a step description.", "Empty Step", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Add the step description to the steps list
            steps.Add(stepDescription);

            // Add the step description to the StepsListBox with a step number
            StepsListBox.Items.Add($"{StepsListBox.Items.Count + 1}. {stepDescription}");

            // Clear the text box
            StepTextBox.Clear();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Event handler for adding a recipe
        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the recipe name from the user
            string recipeName = RecipeNameTextBox.Text;

            // Check if the recipe name is not empty
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                MessageBox.Show("Please enter a recipe name.", "Empty Recipe Name", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if at least one ingredient and step have been added
            if (ingredients.Count == 0 || steps.Count == 0)
            {
                MessageBox.Show("Please add at least one ingredient and step.", "Missing Ingredients or Steps", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create copies of the ingredients and steps collections
            var ingredientsCopy = new ObservableCollection<Ingredient>(ingredients);
            var stepsCopy = new ObservableCollection<string>(steps);

            // Create a new Recipe object with the details entered by the user
            Recipe recipe = new Recipe(recipeName, ingredientsCopy, stepsCopy);

            // Calculate the total calories
            int totalCalories = recipe.CalculateTotalCalories();

            // Display the total calories
            TotalCaloriesLabel.Content = $"Total Calories: {totalCalories}";

            // Check if the total calories exceed 300
            if (totalCalories > 300)
            {
                MessageBox.Show("The total calories of this recipe exceed 300.", "High Calorie Count", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Add the Recipe object to the RecipeManager
            recipeManager.AddRecipe(recipe);

            // Clear the input fields and lists for the next recipe
            RecipeNameTextBox.Clear();
            ingredients.Clear();
            steps.Clear();
            IngredientsDataGrid.Items.Refresh();
            StepsListBox.Items.Clear();

            // Show a message box to confirm that the recipe has been added
            MessageBox.Show("Recipe added successfully!", "Recipe Added", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Event handler for returning to the menu
        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window
            this.Close();

            // Open the Main Menu window with the existing RecipeManager instance
            menuWindow.Show();
        }

        // Event handler for clearing all fields
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the input fields
            RecipeNameTextBox.Clear();

            // Clear the ingredients and steps collections
            ingredients.Clear();
            steps.Clear();

            // Refresh the data grid and list box
            IngredientsDataGrid.Items.Refresh();
            StepsListBox.Items.Clear();

            // Clear the total calories label
            TotalCaloriesLabel.Content = "Total Calories: 0";
        }
    }
}

// ----- End of AddRecipeWindow.xaml.cs -----