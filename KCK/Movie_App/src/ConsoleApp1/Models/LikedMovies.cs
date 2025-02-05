using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class LikedMovie
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
