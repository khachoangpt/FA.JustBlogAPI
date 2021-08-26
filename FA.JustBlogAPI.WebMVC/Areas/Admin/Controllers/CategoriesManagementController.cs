using FA.JustBlogAPI.Common;
using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.WebMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace FA.JustBlogAPI.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesManagementController : Controller
    {
        public ActionResult Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 4)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CategoryNameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "categoryName_desc" : "";
            ViewData["UrlSlugSortParm"] = sortOrder == "UrlSlug" ? "urlSlug_desc" : "UrlSlug";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "description_desc" : "Description";

            Paginated<Category> categories = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"categoriesManagement/index?sortOrder={sortOrder}&currentFilter={currentFilter}&searchString={searchString}&pageIndex={pageIndex}&pageSize={pageSize}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Paginated<Category>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
            }

            ViewData["CurrentFilter"] = searchString;

            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var postTask = client.PostAsJsonAsync<CategoryViewModel>("categoriesManagement/create", categoryViewModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Insert successful!";
                    return RedirectToAction("Index");
                }
            }

            TempData["Message"] = "Insert failed!";

            return View(categoryViewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            CategoryViewModel categoryViewModel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"categoriesManagement/edit?id={id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryViewModel>();
                    readTask.Wait();

                    categoryViewModel = readTask.Result;
                }
            }

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var postTask = client.PostAsJsonAsync<CategoryViewModel>("categoriesManagement/edit", categoryViewModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Update successful!";
                    return RedirectToAction("Index");
                }
            }

            TempData["Message"] = "Update failed!";

            return View(categoryViewModel);
        }

        public ActionResult Delete(Guid? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync($"categoriesManagement/detele/{id}");
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Delete Successful";
                    return RedirectToAction("Index");
                }
            }

            TempData["Message"] = "Delete failed";

            return RedirectToAction("Index");
        }
    }
}
