using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using FA.JustBlogAPI.WebMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlogAPI.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsManagementController : Controller
    {
        private readonly IPostServices _postServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ITagServices _tagServices;

        public PostsManagementController(IPostServices postServices, ICategoryServices categoryServices, ITagServices tagServices)
        {
            _postServices = postServices;
            _categoryServices = categoryServices;
            _tagServices = tagServices;
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 4)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["ImageUrlSortParm"] = sortOrder == "ImageUrl" ? "imageUrl_desc" : "ImageUrl";
            ViewData["PublishedSortParm"] = sortOrder == "Published" ? "published_desc" : "Published";
            ViewData["PostOnSortParm"] = sortOrder == "PostOn" ? "postOn_desc" : "PostOn";
            ViewData["ViewCountSortParm"] = sortOrder == "ViewCount" ? "viewCount_desc" : "ViewCount";
            ViewData["RateCountSortParm"] = sortOrder == "RateCount" ? "rateCount_desc" : "RateCount";
            ViewData["TotalRateSortParm"] = sortOrder == "TotalRate" ? "totalRate_desc" : "TotalRate";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Post, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = p => p.Title.Contains(searchString);
            }

            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;

            switch (sortOrder)
            {
                case "title_desc":
                    orderBy = q => q.OrderByDescending(c => c.Title);
                    break;
                case "Category":
                    orderBy = q => q.OrderBy(c => c.Category.Name);
                    break;
                case "category_desc":
                    orderBy = q => q.OrderByDescending(c => c.Category.Name);
                    break;
                case "UrlSlug":
                    orderBy = q => q.OrderBy(c => c.UrlSlug);
                    break;
                case "urlSlug_desc":
                    orderBy = q => q.OrderByDescending(c => c.UrlSlug);
                    break;
                case "Published":
                    orderBy = q => q.OrderBy(c => c.Published);
                    break;
                case "published_desc":
                    orderBy = q => q.OrderByDescending(c => c.Published);
                    break;
                case "PostOn":
                    orderBy = q => q.OrderBy(c => c.PostedOn);
                    break;
                case "postOn_desc":
                    orderBy = q => q.OrderByDescending(c => c.PostedOn);
                    break;
                case "ViewCount":
                    orderBy = q => q.OrderBy(c => c.ViewCount);
                    break;
                case "viewCount_desc":
                    orderBy = q => q.OrderByDescending(c => c.ViewCount);
                    break;
                case "RateCount":
                    orderBy = q => q.OrderBy(c => c.RateCount);
                    break;
                case "rateCount_desc":
                    orderBy = q => q.OrderByDescending(c => c.RateCount);
                    break;
                case "TotalRate":
                    orderBy = q => q.OrderBy(c => c.TotalRate);
                    break;
                case "totalRate_desc":
                    orderBy = q => q.OrderByDescending(c => c.TotalRate);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Title);
                    break;
            }

            var posts = await _postServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(posts);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryServices.GetAll(), "Id", "Name");
            var postViewModel = new PostViewModel
            {
                Tags = _tagServices.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
            };
            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
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

                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(await _categoryServices.GetAllAsync(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagServices.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            return View(postViewModel);
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

        public async Task<ActionResult> Edit(Guid? id)
        {
            var post = await _postServices.GetByIdAsync((Guid)id);
            var postViewModel = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                UrlSlug = post.UrlSlug,
                ShortDescription = post.ShortDescription,
                ImageUrl = post.ImageUrl,
                PostContent = post.PostContent,
                Published = post.Published,
                ViewCount = post.ViewCount,
                RateCount = post.RateCount,
                TotalRate = post.TotalRate,
                CategoryId = post.CategoryId,
                SelectedTagIds = post.Tags.Select(x => x.Id)
            };
            ViewBag.Categories = new SelectList(_categoryServices.GetAll(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagServices.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            ViewBag.TagList = _tagServices.GetAll();
            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = await _postServices.GetByIdAsync(postViewModel.Id);
                if (post == null)
                {
                    return HttpNotFound();
                }

                post.Title = postViewModel.Title;
                post.UrlSlug = postViewModel.UrlSlug;
                post.ShortDescription = postViewModel.ShortDescription;
                post.ImageUrl = postViewModel.ImageUrl;
                post.PostContent = postViewModel.PostContent;
                post.Published = postViewModel.Published;
                post.CategoryId = postViewModel.CategoryId;
                await UpdateSelectedTagFromIds(postViewModel.SelectedTagIds, post);

                var result = await _postServices.UpdateAsync(post);
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

            ViewBag.Categories = new SelectList(await _categoryServices.GetAllAsync(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagServices.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            ViewBag.TagList = _tagServices.GetAll();

            return View(postViewModel);
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

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _postServices.DeleteAsync((Guid)id);
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
