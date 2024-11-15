using OcenaFilmow.Controllers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcenaFilmow.Views
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
                        .HighlightStyle(new Style(Color.Green)) 
                        );

                switch (userMenuOption)
                {
                    case "1. Display movies":
                        var allMovies = movieController.GetMovies();
                        MovieController.DisplayMovies(movieController, allMovies);
                        break;

                    case "2. Liked movies":
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
                                  .HighlightStyle(new Style(Color.Green)) 
                                  );
                            switch (LikedOption)
                            {
                                case "1. Add to liked movies":
                                    var movieTitle = movieController.SearchMovies(movieController);
                                    if (!string.IsNullOrEmpty(movieTitle))
                                    {
                                        movieController.AddToLikedMovies(username, movieTitle);
                                    }

                                    break;

                                case "2. Check your liked movies":
                                    var likedMovies = movieController.GetLikedMovies(userId);
                                    MovieController.DisplayMovies(movieController, likedMovies);
                                    break;

                                case "3. Back":
                                    exitLikedMovies = true;
                                    break;
                            }
                        }
                        break;

                    case "3. Watchlist":
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
                                  .HighlightStyle(new Style(Color.Green)) 
                                  );
                            switch (WatchOption)
                            {
                                case "1. Add to watchlist":
                                    var movieTitle = movieController.SearchMovies(movieController);
                                    if (!string.IsNullOrEmpty(movieTitle))
                                    {
                                        movieController.AddToWatchList(username, movieTitle);
                                    }

                                    break;

                                case "2. Check your watchlist":
                                    var toWatchMovies = movieController.GetWatchList(userId);
                                    MovieController.DisplayMovies(movieController, toWatchMovies);
                                    break;

                                case "3. Back":
                                    exitWatchList = true;                                  
                                    break;
                            }
                        }
                        break;

                    case "4. Search movie":
                        movieController.SearchMovies(movieController);
                        break;

                    case "5. Top 30 movies":
                        var topMovies = movieController.GetTopMovies();
                        MovieController.DisplayMovies(movieController, topMovies);
                        break;

                    case "6. Log out":
                        exitUserMenu = true;
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
