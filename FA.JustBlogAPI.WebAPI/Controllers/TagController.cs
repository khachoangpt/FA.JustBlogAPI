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
    public class TagController : ApiController
    {
        private readonly ITagServices _tagServices;

        public TagController(ITagServices tagServices)
        {
            _tagServices = tagServices;
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _tagServices.GetAllAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [ResponseType(typeof(Tag))]
        public async Task<IHttpActionResult> GetTag(Guid id)
        {
            var tag = await _tagServices.GetByIdAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTag(TagViewModel tagViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = await _tagServices.GetByIdAsync(tagViewModel.Id);
            if (tag == null)
            {
                return BadRequest();
            }

            tag.Name = tagViewModel.Name;
            tag.UrlSlug = tagViewModel.UrlSlug;
            tag.Description = tagViewModel.Description;
            tag.Count = tagViewModel.Count;

            var result = await _tagServices.UpdateAsync(tag);
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Tag))]
        public async Task<IHttpActionResult> PostTag(TagViewModel tagViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = new Tag()
            {
                Id = tagViewModel.Id,
                Name = tagViewModel.Name,
                UrlSlug = tagViewModel.UrlSlug,
                Description = tagViewModel.Description,
                Count = tagViewModel.Count
            };

            var result = await _tagServices.AddAsync(tag);
            if (result <= 0)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtRoute("DefaultApi", new { id = tag.Id }, tagViewModel);
        }

        [ResponseType(typeof(Tag))]
        public async Task<IHttpActionResult> DeleteTag(Guid id)
        {
            var tag = await _tagServices.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            var result = await _tagServices.DeleteAsync(id);

            if (result)
            {
                return Ok(tag);
            }

            return BadRequest();
        }
    }
}
