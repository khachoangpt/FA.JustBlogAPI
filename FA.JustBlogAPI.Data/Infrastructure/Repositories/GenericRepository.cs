using FA.JustBlogAPI.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Data.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
    {
        protected readonly JustBlogContext _context;
        private readonly DbSet<T> DbSet;

        public GenericRepository(JustBlogContext context)
        {
            _context = context;

            var typeOfDbSet = typeof(DbSet<T>);

            foreach (var prop in context.GetType().GetProperties())
            {
                if (typeOfDbSet == prop.PropertyType)
                {
                    DbSet = prop.GetValue(context, null) as DbSet<T>;
                    break;
                }
            }

            if (DbSet == null)
            {
                DbSet = context.Set<T>();
            }
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity, bool isHardDelete = false)
        {
            if (isHardDelete)
            {
                DbSet.Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(IEnumerable<T> entities, bool isHardDelete = false)
        {
            if (isHardDelete)
            {
                DbSet.RemoveRange(entities);
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                }
            }
        }

        public void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false)
        {
            var entities = GetQuery(where).AsEnumerable();
            Delete(entities, isHardDelete);
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool canLoadDeleted = false)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (canLoadDeleted == false)
            {
                query = query.Where(x => x.IsDeleted == canLoadDeleted);
            }

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query) : query;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual T GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return DbSet;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<IEnumerable<T>> GetByPageAsync(Expression<Func<T, bool>> condition, int size, int page)
        {
            return await DbSet.Where(condition).Skip(size * (page - 1)).Take(size).ToListAsync();
        }
    }
}
