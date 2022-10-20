using MovieAPI.Data;
using MovieAPI.Helper;
using MovieAPI.Interfaces;
using MovieAPI.Model;
using MovieAPI.Response;

namespace MovieAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApiDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public MovieService(ApiDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<MovieResponse> CreateMovieAsync(MovieDTO movieRequest)
        {
            var movie = new Movie
            {
                Country = movieRequest.Country,
                Description = movieRequest.Description,
                ImagePath = movieRequest.ImagePath,
                Name = movieRequest.Name,
                Rating = movieRequest.Rating,
                TicketPrice = movieRequest.TicketPrice,
                ReleaseDate = movieRequest.ReleaseDate.ToString()
            };

            var movieEntry = await _context.Movies.AddAsync(movie);
            var saveResponse = await _context.SaveChangesAsync();
            if (saveResponse < 0) return new MovieResponse { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" };
            var movieEntity = movieEntry.Entity;
            var newMovie = new Movie
            {
                Id = movieEntity.Id,
                Comments = movieEntity.Comments,
                Country = movieEntity.Country,
                Description = movieEntity.Description,
                Genres = movieEntity.Genres,
                ImagePath = movieEntity.ImagePath,
                Name = movieEntity.Name,
                Rating = movieEntity.Rating,
                ReleaseDate = movieEntity.ReleaseDate.ToString()
            };
            return new MovieResponse { Success = true, Movie = newMovie };
        }

        public async Task SaveMovieImageAsync(MovieDTO movie)
        {
            var uniqueFileName = FileHelper.GetUniqueFileName(movie.Image.FileName);
            var uploads = Path.Combine(_environment.WebRootPath, "movie", "images", movie.Name.ToString());
            var filePath = Path.Combine(uploads, uniqueFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            await movie.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            movie.ImagePath = filePath;
            return;
        }
    }
}
