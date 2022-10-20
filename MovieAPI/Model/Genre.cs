using System.Text.Json.Serialization;

namespace MovieAPI.Model
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Movie> Movies { get; set; }
    }
}
