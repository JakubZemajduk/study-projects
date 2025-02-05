using ConsoleApp1.Controllers;
using ConsoleApp1.Data;
using ConsoleApp1.Models;
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
    public partial class WatchlistWindow : Window
    {
        private UserController userController;
        private MovieController movieController;
        private string username;
        private int userId;

        public WatchlistWindow(UserController userController, string username)
        {
            InitializeComponent();
            this.userController = userController;
            this.movieController = new MovieController(new FilmDbContext());
            this.username = username;
            LoadWatchlist();
        }


        private void LoadWatchlist()
        {
            userId = userController.GetUserId(username);
            var watchMovies = movieController.GetWatchList(userId);
            WatchMoviesDataGrid.ItemsSource = watchMovies;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (WatchMoviesDataGrid.SelectedItem is Movie selectedMovie)
            {
                bool removed = movieController.RemoveFromWatchlist(userId, selectedMovie.Id);

                if (removed)
                {
                    MessageBox.Show($"Removed '{selectedMovie.Title}' from watchlist.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadWatchlist();
                }
                else
                {
                    MessageBox.Show("Failed to remove the movie. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a movie to remove.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //UserMenuWindow userMenuWindow = new UserMenuWindow(username);
            //userMenuWindow.Show();
            this.Close();
        }
    }
}
