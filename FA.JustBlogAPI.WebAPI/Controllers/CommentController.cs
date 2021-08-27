using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using FA.JustBlogAPI.WebAPI.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FA.JustBlogAPI.WebAPI.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICommentServices _commentServices;

        public CommentController(CommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _commentServices.GetAllAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(Guid id)
        {
            var comment = await _commentServices.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComment(Guid id, CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentServices.GetByIdAsync(id);
            if (comment == null)
            {
                return BadRequest();
            }

            comment.Name = commentViewModel.Name;
            comment.Email = commentViewModel.Email;
            comment.PostId = commentViewModel.PostId;
            comment.CommentHeader = commentViewModel.CommentHeader;
            comment.CommentText = commentViewModel.CommentText;
            comment.CommentTime = commentViewModel.CommentTime;

            var result = await _commentServices.UpdateAsync(comment);
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostComment(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = new Comment()
            {
                Name = commentViewModel.Name,
                Email = commentViewModel.Email,
                PostId = commentViewModel.PostId,
                CommentHeader = commentViewModel.CommentHeader,
                CommentText = commentViewModel.CommentText,
                CommentTime = commentViewModel.CommentTime
            };

            var result = await _commentServices.AddAsync(comment);
            if (result <= 0)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, commentViewModel);
        }

        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> DeleteComment(Guid id)
        {
            var comment = await _commentServices.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var result = await _commentServices.DeleteAsync(id);

            if (result)
            {
                return Ok(comment);
            }

            return BadRequest();
        }
    }
}
