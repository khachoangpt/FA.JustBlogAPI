using FA.JustBlogAPI.Data.Infrastructure.Repositories;
using FA.JustBlogAPI.Models.BaseEntities;
using FA.JustBlogAPI.Models.Common;
using System;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        JustBlogContext DataContext { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;

        IGenericRepository<Category> CategoryRepository { get; }

        IGenericRepository<Post> PostRepository { get; }

        IGenericRepository<Tag> TagRepository { get; }

        IGenericRepository<Comment> CommentRepository { get; }
    }
}
