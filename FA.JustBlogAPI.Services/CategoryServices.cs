using FA.JustBlogAPI.Data.Infrastructure;
using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services.BaseServices;

namespace FA.JustBlogAPI.Services
{
    public class CategoryServices : BaseServices<Category>, ICategoryServices
    {
        public CategoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
