using OcenaFilmow.Models;
using OcenaFilmow.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using OcenaFilmow.Views;

namespace OcenaFilmow.Controllers
{
    public class MovieController
    {
        private readonly FilmDbContext _context;

        public MovieController(FilmDbContext context)
        {
            _context = context;
        }

        public void AddMovie(string title, string genre, int yearOfProduction, double rating, int numberOfRatings)
        {
            var movie = new Movie(title, genre, yearOfProduction, rating, numberOfRatings);
            _context.Movies.Add(movie);
            _context.SaveChanges(); 
        }

        public List<Movie> GetMovies()
        {
            return _context.Movies.ToList(); 
        }

        public List<Movie> GetTopMovies(int minRatings = 1000, int topCount = 30)
        {
            return _context.Movies
                .Where(m => m.NumberOfRatings >= minRatings)
                .OrderByDescending(m => m.Rating)
                .Take(topCount)
                .ToList();
        }
       public static void DisplayMovies(MovieController movieController, List<Movie> movies)
        {
            int pageNumber = 1;
            const int pageSize = 10;
            bool exitPagination = false;
            List<Movie> filteredMovies = movies;

            while (!exitPagination)
            {
                var pageMovies = filteredMovies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                Console.Clear();
                Console.WriteLine($"Page {pageNumber}:\n");

                if (pageMovies.Count == 0 && pageNumber > 1)
                {
                    Console.WriteLine("No more movies to display.");
                    pageNumber--;
                    continue;
                }

                MovieView.ShowMovies(pageMovies);
                Console.WriteLine("\n Press 'b' for Previous page, 'n' for Next page, 'f' for Filter or 'q' to Quit.");
                var action = Console.ReadKey(intercept: true).KeyChar;

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
                Console.Clear();
            }
        }
        public List<Movie> SearchMoviesByTitle(string titlePrefix)
        {
            return _context.Movies
                           .Where(movie => movie.Title.StartsWith(titlePrefix))
                           .OrderBy(movie => movie.Title)
                           .Take(10)
                           .ToList();
        }

        public string SearchMovies(MovieController movieController)
        {
            Console.Clear();
            Console.WriteLine("Search for a movie by typing its title. Press 'q' to quit.");

            string searchTerm = string.Empty;
            bool searching = true;

            while (searching)
            {
                Console.SetCursorPosition(0, 2);
                Console.Write("Enter title: " + searchTerm.PadRight(Console.WindowWidth - 11));
                var keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Backspace && searchTerm.Length > 0)
                {
                    searchTerm = searchTerm.Substring(0, searchTerm.Length - 1);
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    searching = false;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return searchTerm;
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    searchTerm += keyInfo.KeyChar;
                }

                Console.SetCursorPosition(0, 4);
                Console.WriteLine("Matching movies:\n");

                var matchingMovies = movieController.SearchMoviesByTitle(searchTerm);

                if (matchingMovies.Any())
                {
                    foreach (var movie in matchingMovies)
                    {
                        Console.WriteLine($"{movie.Title} ({movie.YearOfProduction}) - Rating: {movie.Rating}");
                    }
                }
                else
                {
                    Console.WriteLine("No matching movies found.");
                }

                int linesToClear = 10 - matchingMovies.Count;
                for (int i = 0; i < linesToClear; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
            }
            Console.Clear();
            return string.Empty;
        }
        public int GetMovieId(string title)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Title == title);
            if (movie == null)
            {
                return -1;
            }

            return movie.Id;
        }
        public void AddToLikedMovies(string username, string movieTitle)
        {
            Console.Clear();
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            var movieId = new MovieController(_context).GetMovieId(movieTitle);

            if (movieId == -1)
            {
                return;
            }

            if (user != null && movieId != 0)
            {
                var alreadyLiked = _context.LikedMovies.Any(lm => lm.UserId == user.Id && lm.MovieId == movieId);

                if (!alreadyLiked)
                {
                    var likedMovie = new LikedMovie
                    {
                        UserId = user.Id,
                        MovieId = movieId
                    };
                    _context.LikedMovies.Add(likedMovie);
                    _context.SaveChanges();
                    Console.WriteLine("The movie has been added to the liked movies.");
                }
                else
                {
                    Console.WriteLine("The movie is already in your favorites.");
                }
            }
            else
            {
                Console.WriteLine("Please, try again.");
            }           
        }

        public List<Movie> GetLikedMovies(int userId)
        {
            return _context.LikedMovies
                .Where(u => u.UserId == userId)
                .Include(u => u.Movie)
                .Select(u => u.Movie)
                .ToList();
        }
        public void AddToWatchList(string username, string movieTitle)
        {
            Console.Clear();
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            var movieId = new MovieController(_context).GetMovieId(movieTitle);

            if (movieId == -1)
            {
                return;
            }

            if (user != null && movieId != 0)
            {
                var alreadyAdded = _context.Watchlists.Any(lm => lm.UserId == user.Id && lm.MovieId == movieId);

                if (!alreadyAdded)
                {
                    var watchList = new Watchlist
                    {
                        UserId = user.Id,
                        MovieId = movieId
                    };
                    _context.Watchlists.Add(watchList);
                    _context.SaveChanges();
                    Console.WriteLine("The movie has been added to the watchlist.");
                }
                else
                {
                    Console.WriteLine("The movie is already in your watchlist.");
                }
            }
            else
            {
                Console.WriteLine("Please, try again.");
            }
        }
        public List<Movie> GetWatchList(int userId)
        {
            return _context.Watchlists
                .Where(u => u.UserId == userId)
                .Include(u => u.Movie)
                .Select(u => u.Movie)
                .ToList();
        }
        public List<string> GetAllGenres()
        {
            var genres = _context.Movies
                .Select(m => m.Genre) 
                .ToList();            

            return genres
                .SelectMany(g => g.Split(',')) 
                .Select(g => g.Trim())       
                .Distinct()                 
                .OrderBy(g => g)           
                .ToList();
        }
        public List<Movie> FilterMovies(List<Movie> movies, List<string> selectedGenres, int? startYear, int? endYear, double? minRating, double? maxRating)
        {
            var filteredMovies = movies;

            if (selectedGenres != null && selectedGenres.Any())
            {
                filteredMovies = filteredMovies
                    .Where(movie => selectedGenres.Any(genre => movie.Genre
                        .Split(',')
                        .Select(g => g.Trim().ToLower())
                        .Contains(genre.ToLower())))
                    .ToList();
            }

            if (startYear.HasValue)
            {
                filteredMovies = filteredMovies
                    .Where(movie => movie.YearOfProduction >= startYear.Value)
                    .ToList();
            }

            if (endYear.HasValue)
            {
                filteredMovies = filteredMovies
                    .Where(movie => movie.YearOfProduction <= endYear.Value)
                    .ToList();
            }

            if (minRating.HasValue)
            {
                filteredMovies = filteredMovies
                    .Where(movie => movie.Rating >= minRating.Value)
                    .ToList();
            }

            if (maxRating.HasValue)
            {
                filteredMovies = filteredMovies
                    .Where(movie => movie.Rating <= maxRating.Value)
                    .ToList();
            }

            return filteredMovies;
        }
        public static List<Movie> ApplyFilters(MovieController movieController, List<Movie> movies)
        {
            var genreChoices = movieController.GetAllGenres();
            var selectedGenres = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Select genres (use arrows and space to select):")
                    .PageSize(10)
                    .AddChoices(genreChoices));

            int? startYear = AnsiConsole.Confirm("Do you want to specify a start year?")
                ? AnsiConsole.Ask<int>("Enter start year:")
                : (int?)null;

            int? endYear = AnsiConsole.Confirm("Do you want to specify an end year?")
                ? AnsiConsole.Ask<int>("Enter end year:")
                : (int?)null;

            double? minRating = AnsiConsole.Confirm("Do you want to specify a minimum rating?")
                ? AnsiConsole.Ask<double>("Enter minimum rating:")
                : (double?)null;

            double? maxRating = AnsiConsole.Confirm("Do you want to specify a maximum rating?")
                ? AnsiConsole.Ask<double>("Enter maximum rating:")
                : (double?)null;

            var filteredMovies = movieController.FilterMovies(movies, selectedGenres, startYear, endYear, minRating, maxRating);

            Console.WriteLine($"DEBUG: Filtered movies count: {filteredMovies.Count}");
            return filteredMovies;
        }
    }
}
