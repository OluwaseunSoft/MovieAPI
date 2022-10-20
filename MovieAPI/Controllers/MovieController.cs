using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Interfaces;
using MovieAPI.Model;
using MovieAPI.Response;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> SubmitMovie([FromForm] MovieDTO movieDTO)
        {
            try
            {
                if (movieDTO == null)
                {
                    return BadRequest(new MovieResponse { Success = false, ErrorCode = "S01", Error = "Invalid post request" });
                }
                if (string.IsNullOrEmpty(Request.GetMultipartBoundary()))
                {
                    return BadRequest(new MovieResponse { Success = false, ErrorCode = "S02", Error = "Invalid post header" });
                }
                if (movieDTO.Image != null)
                {
                    await _movieService.SaveMovieImageAsync(movieDTO);
                }
                var movieResponse = await _movieService.CreateMovieAsync(movieDTO);
                if (!movieResponse.Success)
                {
                    return NotFound(movieResponse);
                }
                return Ok(movieResponse.Movie);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
            }
            return StatusCode(500);
        }
    }
}
