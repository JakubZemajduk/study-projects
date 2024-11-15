using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OcenaFilmow.Models;
using OcenaFilmow.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace OcenaFilmow.Services
{
    public class ApiService
    {
        private readonly string _apiKey = "JakiesApiKey"; 
        private readonly FilmDbContext _context;

        public ApiService(FilmDbContext context)
        {
            _context = context;
        }

        public async Task FetchAndStoreMovies()
        {
            using var client = new HttpClient();

                var response = await client.GetStringAsync($"https://api.themoviedb.org/3/movie/popular?api_key={_apiKey}&language=en-US&page=11");

                var json = JObject.Parse(response);

            var genresResponse = await client.GetStringAsync($"https://api.themoviedb.org/3/genre/movie/list?api_key={_apiKey}&language=en-US");
                    var genresJson = JObject.Parse(genresResponse);
                    var genreDictionary = genresJson["genres"].ToDictionary(g => (int)g["id"], g => g["name"].ToString());

            foreach (var movie in json["results"])
            {
                string title = movie["title"].ToString();
                var genreIds = movie["genre_ids"].ToObject<List<int>>();
                string genre = string.Join(", ", genreIds.Select(id => genreDictionary.ContainsKey(id) ? genreDictionary[id] : "Unknown"));
                string releaseDateStr = movie["release_date"]?.ToString();
                if (DateTime.TryParse(releaseDateStr, out DateTime releaseDate))
                {
                    int year = releaseDate.Year;
                    double rating = double.Parse(movie["vote_average"].ToString());
                    int voteCount = movie["vote_count"].Value<int>();

                    var newMovie = new Movie(title, genre, year, rating, voteCount)
                    {
                        Title = title,
                        Genre = genre,
                        YearOfProduction = year,
                        Rating = rating,
                        NumberOfRatings = voteCount
                    };

                    _context.Movies.Add(newMovie);
                }
            }
            await _context.SaveChangesAsync();
        }

    }
}
