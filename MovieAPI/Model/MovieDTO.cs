using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieAPI.Model
{
    public class MovieDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }

        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public string Rating { get; set; }
        public string TicketPrice { get; set; }
        public string Country { get; set; }
        public IFormFile Image { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? ImagePath { get; set; }        
    }
}
