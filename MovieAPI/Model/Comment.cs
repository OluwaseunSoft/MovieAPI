using System.Text.Json.Serialization;

namespace MovieAPI.Model
{
    public class Comment
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string CommentBody { get; set; }
        public string DateCreated { get; set; }
        [JsonIgnore]
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
    }
}
