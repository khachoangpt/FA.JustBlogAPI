using FA.JustBlogAPI.Common;
using FA.JustBlogAPI.Data;
using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FA.JustBlogAPI.WebAPI.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostServices _postServices;

        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        public async Task<IHttpActionResult> GetByPaging(string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 4)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            Expression<Func<Post, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = p => p.Title.Contains(searchString);
            }

            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = q => q.OrderByDescending(c => c.PostedOn);

            var posts = await _postServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return Ok(posts);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Detail(int year, int month, string urlSlug)
        {
            var post = await _postServices.FindPostAsync(year, month, urlSlug);

            if (post == null)
            {
                return NotFound();
            }

            post.ViewCount++;
            await _postServices.UpdateAsync(post);

            return Ok(post);
        }

        [HttpGet]
        public IHttpActionResult LastestPost()
        {
            var posts = _postServices.GetLatestPost(3);
            return Ok(posts);
        }

        [HttpGet]
        public IHttpActionResult MostViewedPost()
        {
            var posts = _postServices.GetMostViewedPost(5);
            return Ok(posts);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPostByCategory(string category)
        {
            var posts = await _postServices.GetPostsByCategoryAsync(category);
            return Ok(posts);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPostByTag(string tag)
        {
            var posts = await _postServices.GetPostsByTagAsync(tag);
            return Ok(posts);
        }
    }
}
