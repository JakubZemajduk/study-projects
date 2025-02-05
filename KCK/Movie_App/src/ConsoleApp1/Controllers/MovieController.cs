using ConsoleApp1.Models;
using ConsoleApp1.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Controllers
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

        public List<Movie> GetPageMovies(List<Movie> movies, int pageNumber)
        {
            int PageSize = 10;
            return movies
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
        public List<Movie> SearchMoviesByTitle(string titlePrefix)
        {
            return _context.Movies
                           .Where(movie => movie.Title.StartsWith(titlePrefix))
                           .OrderBy(movie => movie.Title)
                           .Take(10)
                           .ToList();
        }
        public bool AddToLikedMovies(int userId, int movieId)
        {
            bool alreadyLiked = _context.LikedMovies.Any(lm => lm.UserId == userId && lm.MovieId == movieId);

            if (alreadyLiked)
            {
                return false;
            }
            var likedMovie = new LikedMovie
            {
                UserId = userId,
                MovieId = movieId
            };

            _context.LikedMovies.Add(likedMovie);
            _context.SaveChanges();
            return true;
        }

        public List<Movie> GetLikedMovies(int userId)
        {
            return _context.LikedMovies
                .Where(u => u.UserId == userId)
                .Include(u => u.Movie)
                .Select(u => u.Movie)
                .ToList();
        }
        public bool RemoveFromLiked(int userId, int movieId)
        {
            var likedListEntry = _context.LikedMovies.FirstOrDefault(w => w.UserId == userId && w.MovieId == movieId);

            if (likedListEntry != null)
            {
                _context.LikedMovies.Remove(likedListEntry);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool AddToWatchList(int userId, int movieId)
        {
            bool alreadyLiked = _context.LikedMovies.Any(lm => lm.UserId == userId && lm.MovieId == movieId);

            if (alreadyLiked)
            {
                return false;
            }

            var movie = new Watchlist
            {
                UserId = userId,
                MovieId = movieId
            };

            _context.Watchlists.Add(movie);
            _context.SaveChanges();
            return true;
        }
        public List<Movie> GetWatchList(int userId)
        {
            return _context.Watchlists
                .Where(u => u.UserId == userId)
                .Include(u => u.Movie)
                .Select(u => u.Movie)
                .ToList();
        }
        public bool RemoveFromWatchlist(int userId, int movieId)
        {
            var watchlistEntry = _context.Watchlists.FirstOrDefault(w => w.UserId == userId && w.MovieId == movieId);

            if (watchlistEntry != null)
            {
                _context.Watchlists.Remove(watchlistEntry);
                _context.SaveChanges();
                return true;
            }
            return false;
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

    }
}
