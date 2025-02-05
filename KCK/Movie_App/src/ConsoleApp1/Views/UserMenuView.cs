using ConsoleApp1.Controllers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Views
{
    public static class UserMenuView
    {
        public static void DisplayUserMenu(MovieController movieController, UserController userController, string username)
        {
            bool exitUserMenu = false;
            var userId = userController.GetUserId(username);
            while (!exitUserMenu)
            {
                var userMenuOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"Welcome, {username}! Choose an option:")
                        .AddChoices(new[] {
                        "1. Display movies",
                        "2. Liked movies",
                        "3. Watchlist",
                        "4. Search movie",
                        "5. Top 30 movies",
                        "6. Log out"
                        })
                        .HighlightStyle(new Style(Color.Red))
                        );

                switch (userMenuOption)
                {
                    case "1. Display movies":
                        Console.Clear();
                        var allMovies = movieController.GetMovies();
                        MovieView.DisplayMovies(movieController, allMovies);
                        break;

                    case "2. Liked movies":
                        Console.Clear();
                        bool exitLikedMovies = false;
                        while (!exitLikedMovies)
                        {
                            var LikedOption = AnsiConsole.Prompt(
                              new SelectionPrompt<string>()
                                  .Title("Choose an option:")
                                  .AddChoices(new[] {
                                    "1. Add to liked movies",
                                    "2. Check your liked movies",
                                    "3. Back"
                                  })
                                  .HighlightStyle(new Style(Color.Red))
                                  );
                            switch (LikedOption)
                            {
                                case "1. Add to liked movies":
                                    Console.Clear();
                                    MovieView.SearchAndAddToLikedMovies(movieController, userId);
                                    break;

                                case "2. Check your liked movies":
                                    Console.Clear();
                                    var toLikedMovies = movieController.GetLikedMovies(userId);
                                    if (toLikedMovies.Any())
                                    {
                                        MovieView.DisplayLikedMovies(movieController, userId, toLikedMovies);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nYour liked list is empty.");
                                        Console.ReadKey();
                                    }
                                    Console.Clear();
                                    break;

                                case "3. Back":
                                    exitLikedMovies = true;
                                    break;
                            }
                        }
                        break;

                    case "3. Watchlist":
                        Console.Clear();
                        bool exitWatchList = false;
                        while (!exitWatchList)
                        {
                            var WatchOption = AnsiConsole.Prompt(
                              new SelectionPrompt<string>()
                                  .Title("Choose an option:")
                                  .AddChoices(new[] {
                                    "1. Add to watchlist",
                                    "2. Check your watchlist",
                                    "3. Back"
                                  })
                                  .HighlightStyle(new Style(Color.Red))
                                  );
                            switch (WatchOption)
                            {
                                case "1. Add to watchlist":
                                    Console.Clear();
                                    MovieView.SearchAndAddToWatchList(movieController, userId);
                                    break;

                                case "2. Check your watchlist":
                                    Console.Clear();
                                    var toWatchMovies = movieController.GetWatchList(userId);
                                    if (toWatchMovies.Any())
                                    {
                                        MovieView.DisplayWatchlist(movieController, userId, toWatchMovies);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nYour watchlist is empty.");
                                        Console.ReadKey();
                                    }
                                    Console.Clear();
                                    break;

                                case "3. Back":
                                    exitWatchList = true;
                                    break;
                            }
                        }
                        break;

                    case "4. Search movie":
                        Console.Clear();
                        string searchTerm = MovieView.SearchMovies();

                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            var matchingMovies = movieController.SearchMoviesByTitle(searchTerm);
                            MovieView.ShowMatchingMovies(matchingMovies);
                        }
                        Console.Clear();
                        break;

                    case "5. Top 30 movies":
                        Console.Clear();
                        var topMovies = movieController.GetTopMovies();
                        MovieView.DisplayMovies(movieController, topMovies);
                        break;

                    case "6. Log out":
                        Console.Clear();
                        exitUserMenu = true;
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
