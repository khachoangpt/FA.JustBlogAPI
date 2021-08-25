using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Data.Infrastructure.Repositories
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Get a <typeparamref name="T"/> by Id
        /// </summary>
        /// <param name="id">Id of <typeparamref name="T"/></param>
        /// <returns><typeparamref name="T"/></returns>
        T GetById(Guid id);

        /// <summary>
        /// Get a <typeparamref name="T"/> by Id with async
        /// </summary>
        /// <param name="id">Id of <typeparamref name="T"/></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Add a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"><typeparamref name="T"/></param>
        void Add(T entity);

        /// <summary>
        /// Delete a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isHardDelete"></param>
        void Delete(T entity, bool isHardDelete = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isHardDelete"></param>
        void Delete(IEnumerable<T> entities, bool isHardDelete = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isHardDelete"></param>
        void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQuery();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> GetQuery(Expression<Func<T, bool>> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="canLoadDeleted"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool canLoadDeleted = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
    }
}
