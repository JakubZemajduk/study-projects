namespace OcenaFilmow.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int YearOfProduction { get; set; }
        public double Rating { get; set; }
        public int NumberOfRatings { get; set; }

        public Movie(string title, string genre, int yearOfProduction, double rating, int numberOfRatings)
        {
            Title = title;
            Genre = genre;
            YearOfProduction = yearOfProduction;
            Rating = rating;
            NumberOfRatings = numberOfRatings;
        }
    }
}
