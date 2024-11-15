using Microsoft.EntityFrameworkCore;
using OcenaFilmow.Controllers;
using OcenaFilmow.Data;
using OcenaFilmow.Views;
using OcenaFilmow.Services;
using Spectre.Console;
using OcenaFilmow.Models;

class Program
{
    static void Main(string[] args)
    {
        using var context = new FilmDbContext();
        context.Database.EnsureCreated();

        var movieController = new MovieController(context);
        var userController = new UserController(context);
        MainMenuView.DisplayMainMenu(movieController, userController);

        //var apiService = new ApiService(context);
        // await apiService.FetchAndStoreMovies();
    }
}
