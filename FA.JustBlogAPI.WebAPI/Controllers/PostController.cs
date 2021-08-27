using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using FA.JustBlogAPI.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FA.JustBlogAPI.WebAPI.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostServices _postServices;
        private readonly ITagServices _tagServices;

        public PostController(IPostServices postServices, ITagServices tagServices)
        {
            _postServices = postServices;
            _tagServices = tagServices;
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _postServices.GetAllAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> GetPost(Guid id)
        {
            var post = await _postServices.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPost(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postServices.GetByIdAsync(postViewModel.Id);
            if (post == null)
            {
                return BadRequest();
            }

            post.Title = postViewModel.Title;
            post.ShortDescription = postViewModel.ShortDescription;
            post.ImageUrl = postViewModel.ImageUrl;
            post.PostContent = postViewModel.PostContent;
            post.UrlSlug = postViewModel.UrlSlug;
            post.Published = postViewModel.Published;
            post.CategoryId = postViewModel.CategoryId;
            await UpdateSelectedTagFromIds(postViewModel.SelectedTagIds, post);

            var result = await _postServices.UpdateAsync(post);
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = postViewModel.Title,
                UrlSlug = postViewModel.UrlSlug,
                ShortDescription = postViewModel.ShortDescription,
                ImageUrl = postViewModel.ImageUrl,
                PostContent = postViewModel.PostContent,
                Published = postViewModel.Published,
                PostedOn = DateTime.Now,
                Modified = DateTime.Now,
                CategoryId = postViewModel.CategoryId,
                Tags = await GetSelectedTagFromIds(postViewModel.SelectedTagIds)
            };

            var result = await _postServices.AddAsync(post);
            if (result <= 0)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, postViewModel);
        }

        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> DeletePost(Guid id)
        {
            var post = await _postServices.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var result = await _postServices.DeleteAsync(id);

            if (result)
            {
                return Ok(post);
            }

            return BadRequest();
        }

        private async Task UpdateSelectedTagFromIds(IEnumerable<Guid> selectedTagIds, Post post)
        {
            var tags = post.Tags;
            foreach (var item in tags.ToList())
            {
                post.Tags.Remove(item);
            }
            post.Tags = await GetSelectedTagFromIds(selectedTagIds);
        }

        private async Task<IList<Tag>> GetSelectedTagFromIds(IEnumerable<Guid> selectedTagIds)
        {
            var tags = new List<Tag>();

            var tagEntities = await _tagServices.GetAllAsync();

            foreach (var item in tagEntities)
            {
                if (selectedTagIds.Any(x => x == item.Id))
                {
                    tags.Add(item);
                }
            }
            return tags;
        }
    }
}
