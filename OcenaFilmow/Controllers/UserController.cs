using OcenaFilmow.Models;
using System;
using System.Linq;
using OcenaFilmow.Data;
using Microsoft.AspNetCore.Identity;

namespace OcenaFilmow.Controllers
{
    public class UserController
    {
        private readonly FilmDbContext _context;

        public UserController(FilmDbContext context)
        {
            _context = context;
        }

        public bool RegisterUser(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                return false;
            }
            var user = new User(username, password);
            _context.Users.Add(user); 
            _context.SaveChanges(); 
            return true;
        }

        public bool LoginUser(string username, string password)
        {
            return _context.Users.Any(u => u.Username == username && u.Password == password);
        }

        public int GetUserId(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            return user.Id;
        }
    }
}
