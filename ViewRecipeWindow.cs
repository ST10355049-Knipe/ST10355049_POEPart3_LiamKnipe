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
    /// Interaction logic for ViewRecipeWindow.xaml
    /// </summary>
    public partial class ViewRecipeWindow : Window
    {
        private RecipeManager recipeManager;

        public ViewRecipeWindow(RecipeManager recipeManager)
        {
            InitializeComponent();

            this.recipeManager = recipeManager;

            // Populate the ComboBox with the list of recipes, sorted in alphabetical order
            foreach (var recipe in recipeManager.Recipes.OrderBy(r => r.Name))
            {
                RecipeComboBox.Items.Add(recipe.Name);
            }
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected recipe
            string selectedRecipeName = (string)RecipeComboBox.SelectedItem;
            Recipe selectedRecipe = recipeManager.GetRecipeByName(selectedRecipeName);

            // Display the selected recipe
            RecipeNameTextBlock.Text = selectedRecipe.Name;
            IngredientsListView.ItemsSource = selectedRecipe.Ingredients;
            StepsListBox.ItemsSource = selectedRecipe.Steps;
            TotalCaloriesTextBlock.Text = selectedRecipe.TotalCalories.ToString();

            // Display a warning if the total calories exceed 300
            if (selectedRecipe.TotalCalories > 300)
            {
                MessageBox.Show("Warning: This recipe exceeds 300 calories!");
            }
        }

        private void RecipeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected recipe
            string selectedRecipeName = (string)RecipeComboBox.SelectedItem;
            Recipe selectedRecipe = recipeManager.GetRecipeByName(selectedRecipeName);

            // Display the selected recipe
            RecipeTextBox.Text = selectedRecipe.ToString();
        }
    }
}
