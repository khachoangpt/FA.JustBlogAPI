using FA.JustBlogAPI.Common;
using FA.JustBlogAPI.Models.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace FA.JustBlogAPI.WebMVC.Controllers
{
    public class PostController : Controller
    {

        public ActionResult Index(string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 4)
        {
            Paginated<Post> posts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"post/GetByPaging?currentFilter={currentFilter}&searchString={searchString}&pageIndex={pageIndex}&pageSize={pageSize}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Paginated<Post>>();
                    readTask.Wait();

                    posts = readTask.Result;
                }
            }

            ViewData["currentFilter"] = searchString;

            return View(posts);
        }

        public ActionResult Detail(int year, int month, string urlSlug)
        {
            Post post = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"post/Detail?year={year}&month={month}&urlSlug={urlSlug}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Post>();
                    readTask.Wait();

                    post = readTask.Result;
                }
            }

            return View(post);
        }

        public ActionResult LastestPost()
        {
            IEnumerable<Post> posts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"post/lastestPost");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Post>>();
                    readTask.Wait();

                    posts = readTask.Result;
                }
            }

            return PartialView("_LastestPost", posts);
        }

        public ActionResult MostViewedPost()
        {
            IEnumerable<Post> posts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"post/mostViewedPost");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Post>>();
                    readTask.Wait();

                    posts = readTask.Result;
                }
            }

            return PartialView("_MostViewedPost", posts);
        }

        public ActionResult GetPostByCategory(string category)
        {
            IEnumerable<Post> posts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"post/getPostByCategory?category={category}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Post>>();
                    readTask.Wait();

                    posts = readTask.Result;
                }
            }

            return View(posts);
        }

        public ActionResult GetPostByTag(string tag)
        {
            IEnumerable<Post> posts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"post/getPostByTag?tag={tag}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Post>>();
                    readTask.Wait();

                    posts = readTask.Result;
                }
            }

            return View(posts);
        }
    }
}