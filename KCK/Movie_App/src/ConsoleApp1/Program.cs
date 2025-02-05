using ConsoleApp1.Controllers;
using ConsoleApp1.Data;
using ConsoleApp1.Views;
using System;
using System.Diagnostics;
using System.Windows;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }

        static void ShowMenu()
        {
            Console.WriteLine("Choose mode:");
            Console.WriteLine("1. Console Mode");
            Console.WriteLine("2. Graphic Mode (WPF)");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunTextMode();
                    break;
                case "2":
                    RunWpfMode();
                    break;
                default:
                    Console.WriteLine("Invalid option. Exiting...");
                    break;
            }
        }
        
        static void RunTextMode()
        {
            using var context = new FilmDbContext();
            context.Database.EnsureCreated();

            var movieController = new MovieController(context);
            var userController = new UserController(context);
            MainMenuView.DisplayMainMenu(movieController, userController);
        }

        static void RunWpfMode()
        {            
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string wpfAppPath = Path.Combine(basePath, @"..\..\..\..\ConsoleApp1\bin\Debug\net8.0-windows\Debug\net8.0-windows\WpfApp1.exe");

            if (File.Exists(wpfAppPath))
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
