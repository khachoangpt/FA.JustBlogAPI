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
    public class TagsManagementController : Controller
    {
        private readonly ITagServices _tagServices;

        public TagsManagementController(ITagServices tagServices)
        {
            _tagServices = tagServices;
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 4)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TagNameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "tagName_desc" : "";
            ViewData["UrlSlugSortParm"] = sortOrder == "UrlSlug" ? "urlSlug_desc" : "UrlSlug";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "description_desc" : "Description";
            ViewData["CountSortParm"] = sortOrder == "Count" ? "count_desc" : "Count";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Tag, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString);
            }

            Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy = null;

            switch (sortOrder)
            {
                case "tagName_desc":
                    orderBy = q => q.OrderByDescending(t => t.Name);
                    break;
                case "UrlSlug":
                    orderBy = q => q.OrderBy(t => t.UrlSlug);
                    break;
                case "urlSlug_desc":
                    orderBy = q => q.OrderByDescending(t => t.UrlSlug);
                    break;
                case "Description":
                    orderBy = q => q.OrderBy(t => t.Description);
                    break;
                case "description_desc":
                    orderBy = q => q.OrderByDescending(t => t.Description);
                    break;
                case "Count":
                    orderBy = q => q.OrderBy(t => t.Count);
                    break;
                case "count_desc":
                    orderBy = q => q.OrderByDescending(t => t.Count);
                    break;
                default:
                    orderBy = q => q.OrderBy(t => t.Name);
                    break;
            }

            var tags = await _tagServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);
            return View(tags);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = tagViewModel.Name,
                    UrlSlug = tagViewModel.UrlSlug,
                    Description = tagViewModel.Description,
                    Count = tagViewModel.Count
                };
                var result = await _tagServices.AddAsync(tag);
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

            return View(tagViewModel);
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tag = await _tagServices.GetByIdAsync((Guid)id);
            var tagViewModel = new TagViewModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                UrlSlug = tag.UrlSlug,
                Description = tag.Description,
                Count = tag.Count
            };

            return View(tagViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = await _tagServices.GetByIdAsync(tagViewModel.Id);
                if (tag == null)
                {
                    return HttpNotFound();
                }

                tag.Name = tagViewModel.Name;
                tag.UrlSlug = tagViewModel.UrlSlug;
                tag.Description = tagViewModel.Description;
                tag.Count = tagViewModel.Count;

                var result = await _tagServices.UpdateAsync(tag);
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
            return View(tagViewModel);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _tagServices.DeleteAsync((Guid)id);
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
