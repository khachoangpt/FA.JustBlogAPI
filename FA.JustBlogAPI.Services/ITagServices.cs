using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Services
{
    public interface ITagServices : IBaseService<Tag>
    {
        /// <summary>
        /// Get tag by Url Slug
        /// </summary>
        /// <param name="urlSlug">Url Slug of tag</param>
        /// <returns>Tag</returns>
        Tag GetTagByUrlSlug(string urlSlug);

        /// <summary>
        /// Get tag by Url Slug with async
        /// </summary>
        /// <param name="urlSlug">Url Slug of tag</param>
        /// <returns>Tag</returns>
        Task<Tag> GetTagByUrlSlugAsync(string urlSlug);

        /// <summary>
        /// Get popular tag with
        /// </summary>
        /// <param name="size">number of tag want to get</param>
        /// <returns>List of Tag</returns>
        IEnumerable<Tag> GetPopularTags(int size);

        /// <summary>
        /// Get popular tag with async
        /// </summary>
        /// <param name="size">number of tag want to get</param>
        /// <returns>List of Tag</returns>
        Task<IEnumerable<Tag>> GetPopularTagAsync(int size);
    }
}
