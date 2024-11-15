using Spectre.Console;
using OcenaFilmow.Models;

namespace OcenaFilmow.Views
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
    }
}
