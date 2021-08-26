using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using FA.JustBlogAPI.WebMVC.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlogAPI.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommentsManagementController : Controller
    {
        private readonly ICommentServices _commentServices;
        private readonly IPostServices _postServices;

        public CommentsManagementController(ICommentServices commentServices, IPostServices postServices)
        {
            _commentServices = commentServices;
            _postServices = postServices;
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 2)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CommentNameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "commentName_desc" : "";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "email_desc" : "Email";
            ViewData["TitleSortParm"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["CommentHeaderSortParm"] = sortOrder == "CommentHeader" ? "commentHeader_desc" : "CommentHeader";
            ViewData["CommentTextSortParm"] = sortOrder == "CommentText" ? "commentText_desc" : "CommentText";
            ViewData["CommentTimeSortParm"] = sortOrder == "CommentTime" ? "commentTime_desc" : "CommentTime";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Comment, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = p => p.Name.Contains(searchString);
            }

            Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null;

            switch (sortOrder)
            {
                case "commentName_desc":
                    orderBy = q => q.OrderByDescending(c => c.Name);
                    break;
                case "Email":
                    orderBy = q => q.OrderBy(c => c.Email);
                    break;
                case "email_desc":
                    orderBy = q => q.OrderByDescending(c => c.Email);
                    break;
                case "Title":
                    orderBy = q => q.OrderBy(c => c.Post.Title);
                    break;
                case "title_desc":
                    orderBy = q => q.OrderByDescending(c => c.Post.Title);
                    break;
                case "CommentHeader":
                    orderBy = q => q.OrderBy(c => c.CommentHeader);
                    break;
                case "commentHeader_desc":
                    orderBy = q => q.OrderByDescending(c => c.CommentHeader);
                    break;
                case "CommentText":
                    orderBy = q => q.OrderBy(c => c.CommentText);
                    break;
                case "commentText_desc":
                    orderBy = q => q.OrderByDescending(c => c.CommentText);
                    break;
                case "CommentTime":
                    orderBy = q => q.OrderBy(c => c.CommentTime);
                    break;
                case "commentTime_desc":
                    orderBy = q => q.OrderByDescending(c => c.CommentTime);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Name);
                    break;
            }

            var comments = await _commentServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);
            return View(comments);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = commentViewModel.Name,
                    Email = commentViewModel.Email,
                    PostId = commentViewModel.PostId,
                    CommentHeader = commentViewModel.CommentHeader,
                    CommentText = commentViewModel.CommentText,
                    CommentTime = commentViewModel.CommentTime
                };
                var result = await _commentServices.AddAsync(comment);
                if (result > 0)
                {
                    TempData["Message"] = "Insert successful!";
                }
                else
                {
                    TempData["Message"] = "Insert failed!";
                }
                return RedirectToAction("Index");
            }

            return View(commentViewModel);
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = await _commentServices.GetByIdAsync((Guid)id);
            var commentViewModel = new CommentViewModel()
            {
                Id = comment.Id,
                Name = comment.Name,
                Email = comment.Email,
                PostId = comment.PostId,
                CommentHeader = comment.CommentHeader,
                CommentText = comment.CommentText,
                CommentTime = comment.CommentTime
            };

            return View(commentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = await _commentServices.GetByIdAsync(commentViewModel.Id);
                if (comment == null)
                {
                    return HttpNotFound();
                }

                comment.Name = commentViewModel.Name;
                comment.Email = commentViewModel.Email;
                comment.PostId = commentViewModel.PostId;
                comment.CommentHeader = commentViewModel.CommentHeader;
                comment.CommentText = commentViewModel.CommentText;
                comment.CommentTime = commentViewModel.CommentTime;

                var result = await _commentServices.UpdateAsync(comment);
                if (result)
                {
                    TempData["Message"] = "Update successful!";
                }
                else
                {
                    TempData["Message"] = "Update failed!";
                }
                return RedirectToAction("Index");
            }
            return View(commentViewModel);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _commentServices.DeleteAsync((Guid)id);
            if (result)
            {
                TempData["Message"] = "Delete Successful";
            }
            else
            {
                TempData["Message"] = "Delete failed";
            }
            return RedirectToAction("Index");
        }
    }
}
