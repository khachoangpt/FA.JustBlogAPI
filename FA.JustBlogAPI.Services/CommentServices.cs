using FA.JustBlogAPI.Data.Infrastructure;
using FA.JustBlogAPI.Models.Common;
using FA.JustBlogAPI.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Services
{
    public class CommentServices : BaseServices<Comment>, ICommentServices
    {
        public CommentServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override int Add(Comment entity)
        {
            entity.CommentTime = DateTime.Now;
            return base.Add(entity);
        }

        public override Task<int> AddAsync(Comment entity)
        {
            entity.CommentTime = DateTime.Now;
            return base.AddAsync(entity);
        }

        public override bool Update(Comment entity)
        {
            entity.CommentTime = DateTime.Now;
            return base.Update(entity);
        }

        public override Task<bool> UpdateAsync(Comment entity)
        {
            entity.CommentTime = DateTime.Now;
            return base.UpdateAsync(entity);
        }

        public async Task<int> AddCommentAsync(int postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            var comment = new Comment
            {
                PostId = Guid.NewGuid(),
                Name = commentName,
                Email = commentEmail,
                CommentHeader = commentTitle,
                CommentText = commentBody,
                CommentTime = DateTime.Now
            };
            _unitOfWork.CommentRepository.Add(comment);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsForPostAsync(Post post)
        {
            return await _unitOfWork.CommentRepository.GetQuery().Where(x => x.PostId == post.Id).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsForPostAsync(Guid postId)
        {
            return await _unitOfWork.CommentRepository.GetQuery().Where(x => x.PostId == postId).ToListAsync();
        }

        public IEnumerable<Comment> GetCommentsForPost(Guid postId)
        {
            return _unitOfWork.CommentRepository.GetQuery().Where(x => x.PostId == postId).OrderBy(p => p.CommentTime).ToList();
        }
    }
}
