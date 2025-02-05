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
    public partial class SearchMoviesWindow : Window
    {
        private MovieController movieController;
        private UserController userController;
        private int userId;

        private List<Movie> allMovies;

        public SearchMoviesWindow(UserController userController, int userId)
        {
            InitializeComponent();
            this.userController = userController;
            this.movieController = new MovieController(new FilmDbContext());
            this.userId = userId;

            LoadMovies();
        }

        private void LoadMovies()
        {
            allMovies = movieController.GetMovies();
            MoviesDataGrid.ItemsSource = allMovies;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                MoviesDataGrid.ItemsSource = allMovies;
            }
            else
            {
                var filteredMovies = allMovies.Where(m => m.Title.ToLower().Contains(searchText)).ToList();
                MoviesDataGrid.ItemsSource = filteredMovies;
            }
        }

        private void AddToLikedButton_Click(object sender, RoutedEventArgs e)
        {
            if (MoviesDataGrid.SelectedItem is Movie selectedMovie)
            {
                bool added = movieController.AddToLikedMovies(userId, selectedMovie.Id);
                if (added)
                {
                    MessageBox.Show($"Added '{selectedMovie.Title}' to Liked Movies.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to add '{selectedMovie.Title}' to Liked Movies.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a movie first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddToWatchlistButton_Click(object sender, RoutedEventArgs e)
        {
            if (MoviesDataGrid.SelectedItem is Movie selectedMovie)
            {
                bool added = movieController.AddToWatchList(userId, selectedMovie.Id);
                if (added)
                {
                    MessageBox.Show($"Added '{selectedMovie.Title}' to Watchlist.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to add '{selectedMovie.Title}' to Watchlist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a movie first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text.Trim().ToLower();
            var filteredMovies = movieController.GetMovies()
                .Where(m => m.Title.ToLower().Contains(searchQuery))
                .ToList();
            MoviesDataGrid.ItemsSource = filteredMovies;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
