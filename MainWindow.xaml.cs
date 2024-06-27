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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10355049_POEPart3_LiamKnipe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RecipeManager recipeManager;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            // Initialize the RecipeManager
            recipeManager = new RecipeManager();
        }

        // Event handler for going to the menu
        private void GoToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new MenuWindow, passing the RecipeManager to the constructor
            MenuWindow menuWindow = new MenuWindow(recipeManager);

            // Show the MenuWindow
            menuWindow.Show();

            // Close the MainWindow
            this.Close();
        }
    }
}

// ----- End of MainWindow.xaml.cs -----