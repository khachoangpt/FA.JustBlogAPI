using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlogAPI.Common
{
    public class Paginated<T> : List<T>
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        [JsonConstructor]
        public Paginated()
        {

        }

        public Paginated(List<T> entities, long count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(entities);
        }

        public static async Task<Paginated<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize)
        {
            var count = await query.LongCountAsync();

            var entities = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Paginated<T>(entities, count, pageIndex, pageSize);
        }
    }
}
