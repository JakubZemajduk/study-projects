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
    public partial class DisplayTopMoviesWindow : Window
    {
        private int currentPage = 1;
        private const int moviesPerPage = 12;
        private int totalPages;
        private List<Movie> allMovies;
        private List<Movie> _filteredMovies;
        private MovieController movieController;

        public DisplayTopMoviesWindow()
        {
            InitializeComponent();
            movieController = new MovieController(new FilmDbContext());
            InitializeGenres();
            LoadMovies();
        }

        private void LoadMovies()
        {
            allMovies = movieController.GetTopMovies();
            _filteredMovies = new List<Movie>(allMovies);
            totalPages = (int)Math.Ceiling((double)_filteredMovies.Count / moviesPerPage);
            DisplayMovies(currentPage);
        }

        private void DisplayMovies(int page)
        {
            MoviesWrapPanel.Children.Clear();

            var moviesToDisplay = _filteredMovies.Skip((page - 1) * moviesPerPage).Take(moviesPerPage).ToList();

            foreach (var movie in moviesToDisplay)
            {
                var movieBorder = new Border
                {
                    Style = (Style)FindResource("MovieTileStyle"),
                    Child = new StackPanel
                    {
                        Children =
                        {
                            new TextBlock
                            {
                                Text = movie.Title,
                                Style = (Style)FindResource("MovieTextStyle"),
                                TextTrimming = TextTrimming.CharacterEllipsis,
                                 HorizontalAlignment = HorizontalAlignment.Center
                            },
                            new TextBlock
                            {
                                Text = $"Year: {movie.YearOfProduction}",
                                HorizontalAlignment = HorizontalAlignment.Center
                            },
                            new TextBlock
                            {
                                Text = $"Genre: {movie.Genre}",
                                HorizontalAlignment = HorizontalAlignment.Center
                            },
                            new TextBlock
                            {
                                Text = $"Rating: {movie.Rating}",
                                HorizontalAlignment = HorizontalAlignment.Center
                            }
                        }
                    }
                };

                MoviesWrapPanel.Children.Add(movieBorder);
            }

            UpdatePaginationButtons();
        }

        private void UpdatePaginationButtons()
        {
            PreviousButton.IsEnabled = currentPage > 1;
            NextButton.IsEnabled = currentPage < totalPages;
        }
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayMovies(currentPage);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayMovies(currentPage);
            }
        }

        private void PreviousButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Background = (Brush)(new BrushConverter().ConvertFrom("#FF388E3C"));
            }
        }

        private void PreviousButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Background = (Brush)(new BrushConverter().ConvertFrom("#FF4CAF50"));
            }
        }

        private void NextButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Background = (Brush)(new BrushConverter().ConvertFrom("#FF388E3C"));
            }
        }

        private void NextButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Background = (Brush)(new BrushConverter().ConvertFrom("#FF4CAF50"));
            }
        }

        private List<Movie> ApplyFilters()
        {
            var filteredMovies = _filteredMovies.AsQueryable();

            var selectedGenres = GenreListBox.SelectedItems.Cast<ListBoxItem>()
                .Select(item => item.Content.ToString())
                .ToList();

            if (selectedGenres.Any())
            {
                filteredMovies = filteredMovies.Where(m => selectedGenres
                    .Any(genre => m.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase)));
            }

            if (int.TryParse(YearFromTextBox.Text, out int yearFrom))
            {
                filteredMovies = filteredMovies.Where(m => m.YearOfProduction >= yearFrom);
            }

            if (int.TryParse(YearToTextBox.Text, out int yearTo))
            {
                filteredMovies = filteredMovies.Where(m => m.YearOfProduction <= yearTo);
            }

            if (double.TryParse(RatingFromTextBox.Text, out double ratingFrom))
            {
                filteredMovies = filteredMovies.Where(m => m.Rating >= ratingFrom);
            }

            if (double.TryParse(RatingToTextBox.Text, out double ratingTo))
            {
                filteredMovies = filteredMovies.Where(m => m.Rating <= ratingTo);
            }

            return filteredMovies.ToList();
        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            var filteredMovies = ApplyFilters();
            totalPages = (int)Math.Ceiling((double)filteredMovies.Count / moviesPerPage);
            _filteredMovies = filteredMovies;
            currentPage = 1;
            DisplayMovies(currentPage);
        }

        private void InitializeGenres()
        {
            var genres = movieController.GetAllGenres();
            foreach (var genre in genres)
            {
                GenreListBox.Items.Add(new ListBoxItem { Content = genre });
            }
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            GenreListBox.SelectedItems.Clear();

            YearFromTextBox.Clear();
            YearToTextBox.Clear();
            RatingFromTextBox.Clear();
            RatingToTextBox.Clear();

            LoadMovies();

            currentPage = 1;

            DisplayMovies(currentPage);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
