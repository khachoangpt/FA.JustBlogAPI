using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace FA.JustBlogAPI.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Category> categories = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"category/getAllCategory");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Category>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
            }

            return PartialView("_MenuCategory", categories);
        }
    }
}