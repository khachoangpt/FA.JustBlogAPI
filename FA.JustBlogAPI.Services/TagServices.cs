using FA.JustBlogAPI.Data.Infrastructure;
using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services.BaseServices;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Services
{
    public class TagServices : BaseServices<Tag>, ITagServices
    {
        public TagServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Tag>> GetPopularTagAsync(int size)
        {
            return await _unitOfWork.TagRepository.GetQuery().Where(x => x.IsDeleted == false).OrderByDescending(t => t.Posts.Count).Take(size).ToListAsync();
        }

        public IEnumerable<Tag> GetPopularTags(int size)
        {
            return _unitOfWork.TagRepository.GetQuery().Where(x => x.IsDeleted == false).OrderByDescending(t => t.Posts.Count).Take(size).ToList();
        }

        public Tag GetTagByUrlSlug(string urlSlug)
        {
            return _unitOfWork.TagRepository.GetQuery().FirstOrDefault(x => x.UrlSlug == urlSlug);
        }

        public async Task<Tag> GetTagByUrlSlugAsync(string urlSlug)
        {
            return await _unitOfWork.TagRepository.GetQuery().FirstOrDefaultAsync(x => x.UrlSlug == urlSlug);
        }
    }
}
