using ConsoleApp1.Controllers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Views
{
    public static class MainMenuView
    {
        public static void DisplayMainMenu(MovieController movieController, UserController userController)
        {
            Console.Clear();
            bool exit = false;

            while (!exit)
            {
                var font = FigletFont.Load("starwars.flf");

                AnsiConsole.Render(
                    new FigletText(font, "Welcome")
                        .Centered()
                        .Color(Color.Red));

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose an option:")
                        .AddChoices(new[] {
                        "1. Log in / Register",
                        "2. Display movies",
                        "3. Top 30 movies",
                        "4. Change to Graphic Mode",
                        "5. Exit"
                        })
                        .HighlightStyle(new Style(Color.Red))
                        );

                switch (option)
                {
                    case "1. Log in / Register":
                        Console.Clear();
                        string currentUsername = null;
                        bool exitLogin = false;
                        while (!exitLogin)
                        {
                            var loginOption = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("Choose an option:")
                                    .AddChoices(new[] {
                                    "1. Log in",
                                    "2. Register",
                                    "3. Back"
                                    })
                                    .HighlightStyle(new Style(Color.Red))
                                    );

                            switch (loginOption)
                            {
                                case "1. Log in":
                                    Console.Clear();
                                    var username = AnsiConsole.Prompt(new TextPrompt<string>("Enter username:"));
                                    var password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());

                                    if (userController.LoginUser(username, password))
                                    {
                                        AnsiConsole.Status()
                                        .Start("Logging in...", ctx =>
                                        {
                                            Thread.Sleep(2000);
                                        });
                                        AnsiConsole.MarkupLine("[green]Login successful![/]");
                                        currentUsername = username;
                                        UserMenuView.DisplayUserMenu(movieController, userController, currentUsername);
                                        exitLogin = true;
                                    }
                                    else
                                    {
                                        AnsiConsole.Status()
                                        .Start("Logging in...", ctx =>
                                        {
                                            Thread.Sleep(2000);
                                        });
                                        AnsiConsole.MarkupLine("[red]Login failed. Check username and password.[/]");
                                    }
                                    break;

                                case "2. Register":
                                    Console.Clear();
                                    var newUsername = AnsiConsole.Prompt(new TextPrompt<string>("Enter username:"));
                                    var newPassword = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());

                                    if (userController.RegisterUser(newUsername, newPassword))
                                    {
                                        AnsiConsole.Status()
                                        .Start("Logging in...", ctx =>
                                        {
                                            Thread.Sleep(2000);
                                        });
                                        AnsiConsole.MarkupLine("[green]Registration successful![/]");
                                    }
                                    else
                                    {
                                        AnsiConsole.Status()
                                        .Start("Logging in...", ctx =>
                                        {
                                            Thread.Sleep(2000);
                                        });
                                        AnsiConsole.MarkupLine("[red]Registration failed. Username already exists.[/]");
                                    }
                                    break;

                                case "3. Back":
                                    exitLogin = true;
                                    break;
                            }
                        }
                        break;

                    case "2. Display movies":
                        Console.Clear();
                        var allMovies = movieController.GetMovies();
                        MovieView.DisplayMovies(movieController, allMovies);
                        break;

                    case "3. Top 30 movies":
                        Console.Clear();
                        var topMovies = movieController.GetTopMovies();
                        MovieView.DisplayMovies(movieController, topMovies);
                        break;

                    case "4. Change to Graphic Mode":
                        Console.Clear();
                        StartWpfApp();
                        Environment.Exit(0);
                        break;

                    case "5. Exit":
                        exit = true;
                        break;

                    default:
                        Console.Clear();
                        break;
                }
            }
        }
        private static void StartWpfApp()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string wpfAppPath = Path.Combine(basePath, @"..\..\..\..\ConsoleApp1\bin\Debug\net8.0-windows\Debug\net8.0-windows\WpfApp1.exe");

            if (System.IO.File.Exists(wpfAppPath))
            {
                Process.Start(wpfAppPath);
            }
            else
            {
                Console.WriteLine("WpfApp1.exe not found.");
            }
        }
    }
}
