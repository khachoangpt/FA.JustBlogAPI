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
    public class CategoryController : ApiController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _categoryServices.GetAllAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> GetCategory(Guid id)
        {
            var category = await _categoryServices.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategory(Guid id, CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryServices.GetByIdAsync(id);
            if (category == null)
            {
                return BadRequest();
            }

            category.Name = categoryViewModel.Name;
            category.UrlSlug = categoryViewModel.UrlSlug;
            category.Description = categoryViewModel.Description;

            var result = await _categoryServices.UpdateAsync(category);
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> PostCategory(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new Category()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
                UrlSlug = categoryViewModel.UrlSlug,
                Description = categoryViewModel.Description,
            };

            var result = await _categoryServices.AddAsync(category);
            if (result <= 0)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, categoryViewModel);
        }

        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryServices.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var result = await _categoryServices.DeleteAsync(id);

            if (result)
            {
                return Ok(category);
            }

            return BadRequest();
        }
    }
}
