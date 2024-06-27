// ST10355049 PROG6221 POE Liam Knipe

using RecipeManagerWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ST10355049_POEPart3_LiamKnipe
{
    /// <summary>
    /// Interaction logic for DeleteWindow.xaml
    /// </summary>
    public partial class DeleteRecipeWindow : Window
    {
        private RecipeManager recipeManager;

        // Constructor
        public DeleteRecipeWindow(RecipeManager recipeManager)
        {
            InitializeComponent();

            this.recipeManager = recipeManager;

            // Populate the ComboBox with the list of recipes
            foreach (var recipe in recipeManager.Recipes.OrderBy(r => r.Name))
            {
                RecipeComboBox.Items.Add(recipe.Name);
            }
        }

 //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Event handler for deleting a recipe
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected recipe
            string selectedRecipeName = (string)RecipeComboBox.SelectedItem;
            Recipe selectedRecipe = recipeManager.GetRecipeByName(selectedRecipeName);

            // Ask the user to confirm the deletion
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the recipe '{selectedRecipe.Name}'?", "Confirm deletion", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // If the user confirms, delete the recipe
                recipeManager.Recipes.Remove(selectedRecipe);
                MessageBox.Show("Recipe deleted.");
            }
        }

  //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Event handler for returning to the menu
        private void ReturnToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow(recipeManager);
            menuWindow.Show();
            this.Close();
        }

  //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Event handler for exiting the application
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

// ----- End of DeleteRecipeWindow.xaml.cs -----