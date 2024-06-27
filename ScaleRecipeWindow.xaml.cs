// ST10355049 PROG6221 POE Liam Knipe

using RecipeManagerWPF;
using System;
using System.Linq;
using System.Windows;

namespace ST10355049_POEPart3_LiamKnipe
{
    public partial class ScaleRecipeWindow : Window
    {
        private RecipeManager recipeManager;

        // Constructor
        public ScaleRecipeWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            this.recipeManager = recipeManager;

            // Populate RecipeComboBox with the recipes sorted by name
            foreach (var recipe in recipeManager.Recipes.OrderBy(r => r.Name))
            {
                RecipeComboBox.Items.Add(recipe.Name);
            }

            // Populate ScalingFactorComboBox with the scaling factors
            ScalingFactorComboBox.Items.Add(0.5);
            ScalingFactorComboBox.Items.Add(2);
            ScalingFactorComboBox.Items.Add(3);
        }

        // ---------

        // Event handler for scaling recipe
        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedRecipeName = (string)RecipeComboBox.SelectedItem;
            Recipe selectedRecipe = recipeManager.GetRecipeByName(selectedRecipeName);

            if (selectedRecipe == null)
            {
                MessageBox.Show("Please select a recipe.");
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to scale '{selectedRecipe.Name}'?", "Confirm Scaling", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (double.TryParse(ScalingFactorComboBox.SelectedItem.ToString(), out double scalingFactor))
                {
                    selectedRecipe.Scale(scalingFactor);
                    MessageBox.Show($"Recipe '{selectedRecipe.Name}' scaled by a factor of {scalingFactor}.");
                }
                else
                {
                    MessageBox.Show("Please select a valid scaling factor.");
                }
            }
        }

        // ---------

        // Event handler for resetting scale
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Assuming ResetScaleWindow is the separate window you want to navigate to
            ResetScaleWindow resetScaleWindow = new ResetScaleWindow(recipeManager);
            resetScaleWindow.Show();
            this.Close(); // Optionally close the ScaleRecipeWindow
        }

        // ---------

        // Event handler for viewing recipe
        private void ViewRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            ViewRecipeWindow viewRecipeWindow = new ViewRecipeWindow(recipeManager);
            viewRecipeWindow.Show();
            this.Close(); // Optionally close the ScaleRecipeWindow
        }
    }
}

// ----- End of ScaleRecipeWindow.xaml.cs -----