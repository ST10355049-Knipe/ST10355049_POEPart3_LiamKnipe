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
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private static MenuWindow instance;
        private RecipeManager recipeManager;

        // Singleton pattern implementation
        public static MenuWindow GetInstance(RecipeManager recipeManager)
        {
            if (instance == null || !instance.IsLoaded)
            {
                instance = new MenuWindow(recipeManager);
            }
            return instance;
        }

        // Constructor
        public MenuWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            this.recipeManager = recipeManager;
        }

        // ---------

        // Event handler for adding a recipe
        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(recipeManager, this);
            addRecipeWindow.Show();
            this.Hide(); // Hide the MenuWindow
        }

        // ---------

        // Event handler for viewing recipes
        private void ViewRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the ViewRecipeWindow
            ViewRecipeWindow viewRecipeWindow = new ViewRecipeWindow(recipeManager);
            viewRecipeWindow.Show();
        }

        // ---------

        // Event handler for scaling recipes
        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the ScaleRecipeWindow
            ScaleRecipeWindow scaleRecipeWindow = new ScaleRecipeWindow(recipeManager);
            scaleRecipeWindow.Show();
        }

        // ---------

        // Event handler for clearing recipes
        private void ClearRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the DeleteRecipeWindow
            DeleteRecipeWindow deleteRecipeWindow = new DeleteRecipeWindow(recipeManager);
            deleteRecipeWindow.Show();
        }

        // ---------

        // Event handler for resetting recipe scales
        private void ResetScaleButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the ResetScaleWindow
            ResetScaleWindow resetScaleWindow = new ResetScaleWindow(recipeManager);
            resetScaleWindow.Show();
        }

        // ---------

        // Event handler for exiting the application
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }
    }
}

// ----- End of MenuWindow.xaml.cs -----