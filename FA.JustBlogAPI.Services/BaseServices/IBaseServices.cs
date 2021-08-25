using FA.JustBlogAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Services.BaseServices
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Add a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"><typeparamref name="T"/></param>
        /// <returns>int</returns>
        int Add(T entity);

        /// <summary>
        /// Add a <typeparamref name="T"/> with async
        /// </summary>
        /// <param name="entity"><typeparamref name="T"/></param>
        /// <returns>int</returns>
        Task<int> AddAsync(T entity);

        /// <summary>
        /// Add many <typeparamref name="T"/>
        /// </summary>
        /// <param name="entities">List of <typeparamref name="T"/></param>
        /// <returns>int</returns>
        int AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Add many <typeparamref name="T"/> with async
        /// </summary>
        /// <param name="entities">List of <typeparamref name="T"/></param>
        /// <returns>int</returns>
        Task<int> AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Update a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"><typeparamref name="T"/></param>
        /// <returns>bool</returns>
        bool Update(T entity);

        /// <summary>
        /// Update a <typeparamref name="T"/> with async
        /// </summary>
        /// <param name="entity"><typeparamref name="T"/></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Delete <typeparamref name="T"/> by Id
        /// </summary>
        /// <param name="id">Id of <typeparamref name="T"/></param>
        /// <returns>bool</returns>
        bool Delete(Guid id);

        /// <summary>
        /// Delete <typeparamref name="T"/> by Id with async
        /// </summary>
        /// <param name="id">Id of <typeparamref name="T"/></param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Get <typeparamref name="T"/> by Id
        /// </summary>
        /// <param name="id">Id of <typeparamref name="T"/></param>
        /// <returns><typeparamref name="T"/></returns>
        T GetById(Guid id);

        /// <summary>
        /// Get <typeparamref name="T"/> by Id
        /// </summary>
        /// <param name="id">Id of <typeparamref name="T"/></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all <typeparamref name="T"/>
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get all <typeparamref name="T"/> with async
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Return entities with paging, filtering, ordering
        /// </summary>
        /// <param name="filter">x=>x.Name.Contains("abc")</param>
        /// <param name="orderBy">q => q.OrderByDescending(c => c.Name);</param>
        /// <param name="includeProperties">"Products", "Authors, Category, Publisher"</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Paginated<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10);
    }
}
