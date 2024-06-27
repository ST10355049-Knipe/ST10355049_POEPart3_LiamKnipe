// ST10355049 PROG6221 POE Liam Knipe

using RecipeManagerWPF;
using System.Linq;
using System.Windows;

namespace ST10355049_POEPart3_LiamKnipe
{
    public partial class ResetScaleWindow : Window
    {
        private RecipeManager recipeManager;

        // Constructor
        public ResetScaleWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            this.recipeManager = recipeManager;

            // Populate the ComboBox with recipe names
            foreach (var recipe in recipeManager.Recipes.OrderBy(r => r.Name))
            {
                RecipeComboBox.Items.Add(recipe.Name);
            }
        }

        // ---------

        // Event handler for resetting scale
        private void ResetScaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeComboBox.SelectedItem is string selectedRecipeName)
            {
                var selectedRecipe = recipeManager.GetRecipeByName(selectedRecipeName);
                if (selectedRecipe != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to reset the scale for '{selectedRecipe.Name}'?", "Confirm Reset", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        selectedRecipe.ResetScale();
                        MessageBox.Show($"{selectedRecipe.Name}'s scale has been reset.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to reset.");
            }
        }

        // ---------

        // Event handler for viewing recipe
        private void ViewRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the ViewRecipeWindow
            var viewRecipeWindow = new ViewRecipeWindow(recipeManager);
            viewRecipeWindow.Show();
            this.Close();
        }

        // ---------

        // Event handler for returning to menu
        private void ReturnToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var mainMenuWindow = new MenuWindow(recipeManager);
            mainMenuWindow.Show();
            this.Close();
        }
    }
}

// ----- End of ResetScaleWindow.xaml.cs -----