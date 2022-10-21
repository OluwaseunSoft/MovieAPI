using MovieAPI.Model;
using MovieAPI.Response;

namespace MovieAPI.Interfaces
{
    public interface IMovieService
    {
        Task SaveMovieImageAsync(MovieDTO movie);
        Task<MovieResponse> CreateMovieAsync(MovieDTO movie);
        Task<List<Movie>> GetMovies();
    }
}
