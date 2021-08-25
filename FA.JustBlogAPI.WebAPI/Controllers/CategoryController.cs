using FA.JustBlogAPI.Services;
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

        public async Task<IHttpActionResult> GetAllCategory()
        {
            var categories = await _categoryServices.GetAllAsync();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
    }
}
