using MovieAPI.Model;
using MovieAPI.Response;

namespace MovieAPI.Response_
{
    public class CommentResponse : BaseResponse
    {
        public Comment Comment { get; set; }
    }
}
