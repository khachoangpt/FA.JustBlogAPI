using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Services
{
    public interface ICommentServices : IBaseService<Comment>
    {
        /// <summary>
        /// Add a comment
        /// </summary>
        /// <param name="postId">Id of a Post</param>
        /// <param name="commentName">Comment's name</param>
        /// <param name="commentEmail">Comment's email</param>
        /// <param name="commentHeader">Comment's header</param>
        /// <param name="commentText">Comment's text</param>
        /// <returns>int</returns>
        Task<int> AddCommentAsync(int postId, string commentName, string commentEmail, string commentHeader, string commentText);

        /// <summary>
        /// Get Comment by Post Id
        /// </summary>
        /// <param name="postId">Id of Post</param>
        /// <returns>List of Comment</returns>
        IEnumerable<Comment> GetCommentsForPost(Guid postId);

        /// <summary>
        /// Get Comment by Post Id
        /// </summary>
        /// <param name="postId">Id of Post</param>
        /// <returns>List of Comment</returns>
        Task<IEnumerable<Comment>> GetCommentsForPostAsync(Guid postId);

        /// <summary>
        /// Get Comment by Post object
        /// </summary>
        /// <param name="post">a Post object</param>
        /// <returns>Lsit of Comment</returns>
        Task<IEnumerable<Comment>> GetCommentsForPostAsync(Post post);
    }
}
