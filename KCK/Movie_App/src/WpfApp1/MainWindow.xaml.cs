using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConsoleApp1.Controllers;
using ConsoleApp1.Data;
using ConsoleApp1.Models;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        private MovieController movieController;
        public MainWindow()
        {
            InitializeComponent();
            movieController = new MovieController(new FilmDbContext());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = new LoginRegisterWindow();
            user.Show();
        }

        private void DisplayMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            var displayMoviesWindow = new DisplayMoviesWindow();
            displayMoviesWindow.Show();
        }

        private void TopMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            var displayMoviesWindow = new DisplayTopMoviesWindow();
            displayMoviesWindow.Show();
        }
        private void ChangeToConsoleMode_Click(object sender, RoutedEventArgs e)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string consoleAppPath = System.IO.Path.Combine(basePath, @"..\..\..\net8.0-windows\ConsoleApp1.exe");

            Application.Current.Shutdown();

            Process.Start(consoleAppPath); 
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}