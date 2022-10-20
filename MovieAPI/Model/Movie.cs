namespace MovieAPI.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public string Rating { get; set; }
        public string TicketPrice { get; set; }
        public string Country { get; set; }
        public string ImagePath { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
