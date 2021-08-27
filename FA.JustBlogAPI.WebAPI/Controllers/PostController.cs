using FA.JustBlogAPI.Common;
using FA.JustBlogAPI.Data;
using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FA.JustBlogAPI.WebAPI.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostServices _postServices;

        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }
    }
}
