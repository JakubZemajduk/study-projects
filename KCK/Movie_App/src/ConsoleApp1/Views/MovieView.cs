using ConsoleApp1.Controllers;
using ConsoleApp1.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Views
{
    public static class MovieView
    {
        public static void ShowMovies(List<Movie> movies)
        {
            var table = new Table().Centered();
            table.AddColumn("Title");
            table.AddColumn("Genre");
            table.AddColumn("Year of Production");
            table.AddColumn("Rating");

            foreach (var movie in movies)
            {
                string formattedRating = movie.Rating.ToString("F1");
                table.AddRow(movie.Title, movie.Genre, movie.YearOfProduction.ToString(), formattedRating);
            }

            AnsiConsole.Write(table);
        }

        public static char GetPaginationAction()
        {
            Console.WriteLine("\nPress 'b' for Previous page, 'n' for Next page, 'f' for Filter or 'q' to Quit.");
            var action = Console.ReadKey(intercept: true).KeyChar;
            Console.Clear();

            return action;
        }

        public static string SearchMovies()
        {
            string searchTerm = string.Empty;
            Console.Clear();
            Console.WriteLine("Search for a movie by typing its title. Press 'Enter' to search or 'q' to quit.");

            while (true)
            {
                Console.SetCursorPosition(0, 2);
                Console.Write("Enter title: " + searchTerm.PadRight(Console.WindowWidth - 12));
                var keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Q)
                {
                    return string.Empty;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && searchTerm.Length > 0)
                {
                    searchTerm = searchTerm.Substring(0, searchTerm.Length - 1);
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return searchTerm;
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    searchTerm += keyInfo.KeyChar;
                }
            }
        }

        public static void ShowMatchingMovies(List<Movie> matchingMovies)
        {
            Console.Clear();

            if (matchingMovies.Any())
            {
                var table = new Table().Centered();
                table.AddColumn("Title");
                table.AddColumn("Year");
                table.AddColumn("Rating");

                foreach (var movie in matchingMovies)
                {
                    table.AddRow(movie.Title, movie.YearOfProduction.ToString(), movie.Rating.ToString("F1"));
                }

                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine("No matching movies found.");
            }
        }

        public static void SearchAndAddToLikedMovies(MovieController movieController, int userId)
        {
            string searchTerm = string.Empty;

            while (true)
            {
                searchTerm = SearchMovies();
                if (string.IsNullOrEmpty(searchTerm))
                    break;

                var matchingMovies = movieController.SearchMoviesByTitle(searchTerm);
                if (matchingMovies.Any())
                {
                    var selectedMovie = AnsiConsole.Prompt(
                        new SelectionPrompt<Movie>()
                            .Title("Select a movie to add to your liked list:")
                            .AddChoices(matchingMovies)
                            .UseConverter(m => $"{m.Title} ({m.YearOfProduction}) - Rating: {m.Rating:F1}")
                    );

                    bool addedSuccessfully = movieController.AddToLikedMovies(userId, selectedMovie.Id);
                    if (addedSuccessfully)
                    {
                        Console.WriteLine($"\nAdded \"{selectedMovie.Title}\" to your liked movies.");
                    }
                    else
                    {
                        Console.WriteLine($"\nThe movie \"{selectedMovie.Title}\" is already in your liked movies.");
                    }
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("\nNo matching movies found. Try again.");
                }
            }
        }
        public static void DisplayLikedMovies(MovieController movieController, int userId, List<Movie> movies)
        {
            int currentIndex = 0;

            Console.Clear();
            Console.WriteLine("Use arrow keys to navigate, press 'D' to delete a movie, or 'ESC' to go back.\n");

            void RefreshDisplay()
            {
                Console.SetCursorPosition(0, 2);
                for (int i = 0; i < movies.Count; i++)
                {
                    if (i == currentIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {movies[i].Title} ({movies[i].YearOfProduction}) - Rating: {movies[i].Rating:F1}  ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {movies[i].Title} ({movies[i].YearOfProduction}) - Rating: {movies[i].Rating:F1}  ");
                    }
                }
            }

            RefreshDisplay();

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentIndex = (currentIndex == 0) ? movies.Count - 1 : currentIndex - 1;
                        RefreshDisplay();
                        break;

                    case ConsoleKey.DownArrow:
                        currentIndex = (currentIndex == movies.Count - 1) ? 0 : currentIndex + 1;
                        RefreshDisplay();
                        break;

                    case ConsoleKey.D:
                        var movieToRemove = movies[currentIndex];
                        if (movieController.RemoveFromLiked(userId, movieToRemove.Id))
                        {
                            movies.RemoveAt(currentIndex);
                            currentIndex = Math.Min(currentIndex, movies.Count - 1);
                            Console.Clear();
                            Console.WriteLine("Use arrow keys to navigate, press 'D' to delete a movie, or 'ESC' to go back.\n");

                            if (movies.Count == 0)
                            {
                                Console.WriteLine("Your watchlist is empty.");
                                return;
                            }
                            RefreshDisplay();
                        }
                        else
                        {
                            Console.WriteLine("\nFailed to remove the movie. Press any key to continue...");
                            Console.ReadKey(true);
                        }
                        break;

                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
        public static void SearchAndAddToWatchList(MovieController movieController, int userId)
        {
            string searchTerm = string.Empty;

            while (true)
            {
                searchTerm = SearchMovies();
                if (string.IsNullOrEmpty(searchTerm))
                    break;

                var matchingMovies = movieController.SearchMoviesByTitle(searchTerm);
                if (matchingMovies.Any())
                {
                    var selectedMovie = AnsiConsole.Prompt(
                        new SelectionPrompt<Movie>()
                            .Title("Select a movie to add to your watchlist:")
                            .AddChoices(matchingMovies)
                            .UseConverter(m => $"{m.Title} ({m.YearOfProduction}) - Rating: {m.Rating:F1}")
                    );

                    bool addedSuccessfully = movieController.AddToWatchList(userId, selectedMovie.Id);
                    if (addedSuccessfully)
                    {
                        Console.WriteLine($"\nAdded \"{selectedMovie.Title}\" to your watch list.");
                    }
                    else
                    {
                        Console.WriteLine($"\nThe movie \"{selectedMovie.Title}\" is already in your watch list.");
                    }
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("\nNo matching movies found. Try again.");
                }
            }
        }
        public static void DisplayWatchlist(MovieController movieController, int userId, List<Movie> movies)
        {
            int currentIndex = 0;

            Console.Clear();
            Console.WriteLine("Use arrow keys to navigate, press 'D' to delete a movie, or 'ESC' to go back.\n");

            void RefreshDisplay()
            {
                Console.SetCursorPosition(0, 2);
                for (int i = 0; i < movies.Count; i++)
                {
                    if (i == currentIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {movies[i].Title} ({movies[i].YearOfProduction}) - Rating: {movies[i].Rating:F1}  ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {movies[i].Title} ({movies[i].YearOfProduction}) - Rating: {movies[i].Rating:F1}  ");
                    }
                }
            }

            RefreshDisplay();

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentIndex = (currentIndex == 0) ? movies.Count - 1 : currentIndex - 1;
                        RefreshDisplay();
                        break;

                    case ConsoleKey.DownArrow:
                        currentIndex = (currentIndex == movies.Count - 1) ? 0 : currentIndex + 1;
                        RefreshDisplay();
                        break;

                    case ConsoleKey.D:
                        var movieToRemove = movies[currentIndex];
                        if (movieController.RemoveFromWatchlist(userId, movieToRemove.Id))
                        {
                            movies.RemoveAt(currentIndex);
                            currentIndex = Math.Min(currentIndex, movies.Count - 1);
                            Console.Clear();
                            Console.WriteLine("Use arrow keys to navigate, press 'D' to delete a movie, or 'ESC' to go back.\n");

                            if (movies.Count == 0)
                            {
                                Console.WriteLine("Your watchlist is empty.");
                                return;
                            }
                            RefreshDisplay();
                        }
                        else
                        {
                            Console.WriteLine("\nFailed to remove the movie. Press any key to continue...");
                            Console.ReadKey(true);
                        }
                        break;

                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        public static List<Movie> ApplyFilters(MovieController movieController, List<Movie> movies)
        {
            List<string> selectedGenres = null;
            int? startYear = null;
            int? endYear = null;
            double? minRating = null;
            double? maxRating = null;

            bool isFiltering = true;

            while (isFiltering)
            {
                var filterChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select a filter to apply:")
                        .AddChoices("Genres", "Production Year", "Movie Rating", "Apply Filters and Show Movies"));

                switch (filterChoice)
                {
                    case "Genres":
                        selectedGenres = AnsiConsole.Prompt(
                            new MultiSelectionPrompt<string>()
                                .Title("Select genres (use arrows and space to select, press Enter to finish):")
                                .PageSize(10)
                                .AddChoices(movieController.GetAllGenres()));
                        break;

                    case "Production Year":
                        startYear = GetYearInput("Enter the start year (press ESC to skip):");

                        endYear = GetYearInput("Enter the end year (press ESC to skip):");
                        break;

                    case "Movie Rating":
                        minRating = GetRatingInput("Enter minimum rating (press ESC to skip):");

                        maxRating = GetRatingInput("Enter maximum rating (press ESC to skip):");
                        break;

                    case "Apply Filters and Show Movies":
                        isFiltering = false;
                        break;
                }
            }

            var filteredMovies = movieController.FilterMovies(movies, selectedGenres, startYear, endYear, minRating, maxRating);

            return filteredMovies;
        }

        private static int? GetYearInput(string prompt)
        {
            int? year = null;

            while (year == null)
            {
                Console.WriteLine(prompt);

                string input = "";

                while (true)
                {
                    var key = Console.ReadKey(intercept: true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        return null;
                    }

                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }

                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (Char.IsDigit(key.KeyChar))
                    {
                        input += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (input.Length != 4 || !int.TryParse(input, out var parsedYear) || parsedYear > 2024)
                {
                    Console.WriteLine("\nInvalid year! Please enter a valid year (4 digits, no later than 2024).");
                    continue;
                }

                year = int.Parse(input);
            }
            Console.Clear();
            return year;
        }

        private static double? GetRatingInput(string prompt)
        {
            double? rating = null;

            while (rating == null)
            {
                Console.WriteLine(prompt);

                string input = "";

                while (true)
                {
                    var key = Console.ReadKey(intercept: true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        return null;
                    }

                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }

                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (Char.IsDigit(key.KeyChar) || key.KeyChar == ',')
                    {
                        input += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (!double.TryParse(input.Replace('.', ','), out var parsedRating))
                {
                    Console.WriteLine("\nInvalid rating! Please enter a valid number with a decimal point (use a comma, not a dot).");
                    continue;
                }

                rating = parsedRating;
            }
            Console.Clear();
            return rating;
        }
        public static void DisplayMovies(MovieController movieController, List<Movie> movies)
        {
            int pageNumber = 1;
            bool exitPagination = false;
            List<Movie> filteredMovies = movies;

            while (!exitPagination)
            {
                var pageMovies = movieController.GetPageMovies(filteredMovies, pageNumber);

                if (pageMovies.Count == 0 && pageNumber > 1)
                {
                    pageNumber--;
                    continue;
                }

                ShowMovies(pageMovies);

                var action = GetPaginationAction();

                switch (action)
                {
                    case 'n':
                        pageNumber++;
                        break;
                    case 'b':
                        if (pageNumber > 1)
                            pageNumber--;
                        break;
                    case 'f':
                        filteredMovies = ApplyFilters(movieController, movies);
                        pageNumber = 1;
                        break;
                    case 'q':
                        exitPagination = true;
                        break;
                }
            }
        }
    }
}
