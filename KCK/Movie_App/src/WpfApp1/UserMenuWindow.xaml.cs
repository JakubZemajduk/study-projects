using ConsoleApp1.Controllers;
using ConsoleApp1.Data;
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

namespace WpfApp1
{
    public partial class UserMenuWindow : Window
    {
        private string Username;
        private string username;
        private readonly UserController userController;
        private readonly MovieController movieController;
        public UserMenuWindow(string username)
        {
            InitializeComponent();
            Username = username;
            UserController userController = new UserController(new FilmDbContext());
            LikedMoviesWindow likedMoviesWindow = new LikedMoviesWindow(userController, Username);
        }

        private void LikedMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            UserController userController = new UserController(new FilmDbContext());
            LikedMoviesWindow likedMoviesWindow = new LikedMoviesWindow(userController, Username);
            likedMoviesWindow.Show();
            //this.Close();
        }

        private void WatchlistButton_Click(object sender, RoutedEventArgs e)
        {
            UserController userController = new UserController(new FilmDbContext());
            WatchlistWindow watchlistWindow = new WatchlistWindow(userController, Username);
            watchlistWindow.Show();
            //this.Close();
        }

        private void DisplayMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayMoviesWindow displayMoviesWindow = new DisplayMoviesWindow();
            displayMoviesWindow.Show();
        }

        private void SearchMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            UserController userController = new UserController(new FilmDbContext());
            var userId = userController.GetUserId(Username);
            SearchMoviesWindow searchMoviesWindow = new SearchMoviesWindow(userController, userId);
            searchMoviesWindow.Show();
            // this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.LoggedInUser = null;
            MessageBox.Show("You have been logged out.");
            this.Close();
            //OpenMainMenu();
        }

        private void OpenMainMenu()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
