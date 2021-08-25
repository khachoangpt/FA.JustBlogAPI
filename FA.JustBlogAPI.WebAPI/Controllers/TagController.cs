using FA.JustBlogAPI.Services;
using System.Linq;
using System.Web.Http;

namespace FA.JustBlogAPI.WebAPI.Controllers
{
    public class TagController : ApiController
    {
        private readonly ITagServices _tagServices;

        public TagController(ITagServices tagServices)
        {
            _tagServices = tagServices;
        }

        [HttpGet]
        public IHttpActionResult PopularTags()
        {
            var tags = _tagServices.GetAll().Take(10);
            return Ok(tags);
        }
    }
}
