using MovieAPI.Model;
using MovieAPI.Response_;

namespace MovieAPI.Interfaces
{
    public interface ICommentService
    {
        Task<CommentResponse> AddCommentAsync(CommentDTO comment);
    }
}
