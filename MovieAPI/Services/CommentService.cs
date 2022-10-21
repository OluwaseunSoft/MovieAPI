using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Interfaces;
using MovieAPI.Model;
using MovieAPI.Response_;

namespace MovieAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApiDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CommentService(ApiDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<CommentResponse> AddCommentAsync(CommentDTO commentDTO)
        {
            try
            {
                var movie = await _context.Movies.Where(c => c.Id == commentDTO.MovieId).FirstOrDefaultAsync();
                if (movie == null)
                    return new CommentResponse { Success = false, Error = "Movie not available", ErrorCode = "CP01" };
                var comment = new Comment
                {
                    CommentBody = commentDTO.CommentBody,
                    DateCreated = DateTime.Now.ToString("MM/dd/yyyy"),
                    MovieId = commentDTO.MovieId,
                    Name = commentDTO.Name
                };

                var commentEntry = await _context.Comments.AddAsync(comment);
                var saveResponse = await _context.SaveChangesAsync();
                if (saveResponse < 0) return new CommentResponse { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" };
                var commentEntity = commentEntry.Entity;
                var newComment = new Comment
                {
                    Id = commentEntity.Id,
                    DateCreated = commentEntity.DateCreated,
                    CommentBody = commentEntity.CommentBody,
                    MovieId = commentEntity.MovieId,
                    Name = commentEntity.Name,
                    Movie = commentEntity.Movie
                };
                return new CommentResponse { Success = true, Comment = newComment };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return new CommentResponse { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" };
        }
    }
}
