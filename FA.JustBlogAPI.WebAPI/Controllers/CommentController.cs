using FA.JustBlogAPI.Services;
using System;
using System.Web.Http;

namespace FA.JustBlogAPI.WebAPI.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICommentServices _commentServices;

        public CommentController(CommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        public IHttpActionResult GetCommentsForPost(Guid id)
        {
            var comments = _commentServices.GetCommentsForPost(id);
            return Ok(comments);
        }
    }
}
