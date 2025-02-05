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
    public partial class LikedMoviesWindow : Window
    {
        private UserController userController;
        private MovieController movieController;
        private string username;
        private int userId;

        public LikedMoviesWindow(UserController userController, string username)
        {
            InitializeComponent();
            this.userController = userController;
            this.movieController = new MovieController(new FilmDbContext());
            this.username = username;
            LoadLikedMovies();
        }

        private void LoadLikedMovies()
        {
            userId = userController.GetUserId(username);
            var likedMovies = movieController.GetLikedMovies(userId);
            LikedMoviesDataGrid.ItemsSource = likedMovies;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (LikedMoviesDataGrid.SelectedItem is Movie selectedMovie)
            {
                bool removed = movieController.RemoveFromLiked(userId, selectedMovie.Id);

                if (removed)
                {
                    MessageBox.Show($"Removed '{selectedMovie.Title}' from Liked Movies.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadLikedMovies();
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
