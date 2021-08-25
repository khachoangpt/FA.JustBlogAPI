using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace FA.JustBlogAPI.WebMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentServices _commentServices;

        public CommentController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        public ActionResult Index(Guid id)
        {
            IEnumerable<Comment> comments = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"comment/GetCommentsForPost?id={id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Comment>>();
                    readTask.Wait();

                    comments = readTask.Result;
                }
            }

            return PartialView("_ShowComments", comments);
        }

        [HttpPost]
        public JsonResult AddComment(Guid postId, string name, string email, string commentHeader, string commentText)
        {
            var comment = new Comment();
            comment.Id = Guid.NewGuid();
            comment.Name = name;
            comment.Email = email;
            comment.CommentHeader = commentHeader;
            comment.CommentText = commentText;
            comment.CommentTime = DateTime.Now;
            comment.PostId = postId;

            _commentServices.Add(comment);

            return Json(new { comment.Name, comment.CommentHeader, comment.CommentText, comment.CommentTime }, JsonRequestBehavior.AllowGet);
        }
    }
}