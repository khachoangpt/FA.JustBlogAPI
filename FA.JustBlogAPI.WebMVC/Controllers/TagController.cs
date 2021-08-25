using FA.JustBlogAPI.Models.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace FA.JustBlogAPI.WebMVC.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult PopularTags()
        {
            IEnumerable<Tag> tags = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var responseTask = client.GetAsync($"tag/PopularTags");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Tag>>();
                    readTask.Wait();

                    tags = readTask.Result;
                }
            }

            return PartialView("_PopularTags", tags);
        }
    }
}