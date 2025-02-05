using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data
{
    public class FilmDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<LikedMovie> LikedMovies { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source= OcenaFilmow.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Movie>().ToTable("Movies");
        }
    }
}
