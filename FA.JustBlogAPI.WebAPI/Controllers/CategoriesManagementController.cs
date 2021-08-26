using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using FA.JustBlogAPI.WebMVC.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace FA.JustBlogAPI.WebAPI.Controllers
{
    public class CategoriesManagementController : ApiController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesManagementController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Index(string sortOrder, string currentFilter, string searchString,
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

            Expression<Func<Category, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString);
            }

            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

            switch (sortOrder)
            {
                case "categoryName_desc":
                    orderBy = q => q.OrderByDescending(c => c.Name);
                    break;
                case "UrlSlug":
                    orderBy = q => q.OrderBy(c => c.UrlSlug);
                    break;
                case "urlSlug_desc":
                    orderBy = q => q.OrderByDescending(c => c.UrlSlug);
                    break;
                case "Description":
                    orderBy = q => q.OrderBy(c => c.Description);
                    break;
                case "description_desc":
                    orderBy = q => q.OrderByDescending(c => c.Description);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Name);
                    break;
            }
            var categories = await _categoryServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid Category");
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryViewModel.Name,
                UrlSlug = categoryViewModel.UrlSlug,
                Description = categoryViewModel.Description
            };
            var result = await _categoryServices.AddAsync(category);

            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("Not a valid Id");
            }

            var category = await _categoryServices.GetByIdAsync((Guid)id);
            var categoryViewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                UrlSlug = category.UrlSlug,
                Description = category.Description
            };

            return Ok(categoryViewModel);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid data");
            }

            var category = await _categoryServices.GetByIdAsync(categoryViewModel.Id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryViewModel.Name;
            category.UrlSlug = categoryViewModel.UrlSlug;
            category.Description = categoryViewModel.Description;

            var result = await _categoryServices.UpdateAsync(category);

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("Not a valid Id");
            }

            var result = await _categoryServices.DeleteAsync((Guid)id);

            return Ok();
        }
    }
}
