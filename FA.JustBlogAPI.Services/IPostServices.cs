using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Services
{
    public interface IPostServices : IBaseService<Post>
    {
        /// <summary>
        /// Get Published Post or Unpublished Post
        /// </summary>
        /// <param name="published">Published or Unpublished</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPublisedPostsAsync(bool published = true);

        /// <summary>
        /// Get Lastest Post with Async
        /// </summary>
        /// <param name="size">number of Post want to get</param>
        /// <param name="published">Published or Unpublished</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetLatestPostAsync(int size, bool published = true);

        /// <summary>
        /// Get Lastest Post
        /// </summary>
        /// <param name="size">number of Post want to get</param>
        /// <param name="published">Published or Unpublished</param>
        /// <returns>List of Post</returns>
        IEnumerable<Post> GetLatestPost(int size, bool published = true);

        /// <summary>
        /// Get Post by Month
        /// </summary>
        /// <param name="monthYear">Date</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPostsByMonthAsync(DateTime monthYear);

        /// <summary>
        /// Find Post by Year, Month and Title with Async
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="title">Title</param>
        /// <returns></returns>
        Task<Post> FindPostAsync(int year, int month, string urlSlug);

        /// <summary>
        /// Count Post By Category with Async
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>int</returns>
        Task<int> CountPostsForCategoryAsync(string category);

        /// <summary>
        /// Get Post By Category with Async
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(string category);

        /// <summary>
        /// Get Post By Category Id with async
        /// </summary>
        /// <param name="categoryId">Id of Category</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(Guid categoryId);

        /// <summary>
        /// Count Post By Tag with async
        /// </summary>
        /// <param name="tag">tag's name</param>
        /// <returnsint>int</returns>
        Task<int> CountPostsForTagAsync(string tag);

        /// <summary>
        /// Get Post by Tag with async
        /// </summary>
        /// <param name="tag">tag name</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPostsByTagAsync(string tag);

        /// <summary>
        /// Get Post by Tag Id
        /// </summary>
        /// <param name="tagId">Id of Tag</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPostsByTagAsync(Guid tagId);

        /// <summary>
        /// Get most view Post with async
        /// </summary>
        /// <param name="size">number of Post want to get</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetMostViewedPostAsync(int size);

        /// <summary>
        /// Get most view Post
        /// </summary>
        /// <param name="size">number of Post want to get</param>
        /// <returns>List of Post</returns>
        IEnumerable<Post> GetMostViewedPost(int size);

        /// <summary>
        /// Get highest Posts
        /// </summary>
        /// <param name="size">number of Post want to get</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetHighestPosts(int size);

    }
}
