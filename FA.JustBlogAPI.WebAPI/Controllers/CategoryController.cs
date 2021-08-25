using FA.JustBlogAPI.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace FA.JustBlogAPI.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public async Task<IHttpActionResult> GetById(Guid id)
        {
            var category = await _categoryServices.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
