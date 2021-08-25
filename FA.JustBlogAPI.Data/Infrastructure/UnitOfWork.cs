using FA.JustBlogAPI.Data.Infrastructure.Repositories;
using FA.JustBlogAPI.Models.BaseEntities;
using FA.JustBlogAPI.Models.Common;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JustBlogContext _dbContext;

        public JustBlogContext DataContext => _dbContext;

        public UnitOfWork(JustBlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IGenericRepository<Category> _categoryRepository;

        public IGenericRepository<Category> CategoryRepository => _categoryRepository ?? new GenericRepository<Category>(_dbContext);

        private IGenericRepository<Post> _postRepository;

        public IGenericRepository<Post> PostRepository => _postRepository ?? new GenericRepository<Post>(_dbContext);

        private IGenericRepository<Tag> _tagRepository;

        public IGenericRepository<Tag> TagRepository => _tagRepository ?? new GenericRepository<Tag>(_dbContext);

        private IGenericRepository<Comment> _commentRepository;

        public IGenericRepository<Comment> CommentRepository => _commentRepository ?? new GenericRepository<Comment>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(_dbContext);
        }
    }
}
