using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Extensions.Collections
{
    public static class CollectionExtensions
    {
        public static async Task<CollectionResult<T>> AsCollection<T>(this IQueryable<T> query, int offset, int count)
        {
            var totalCount = await query.CountAsync();
            var data = await query.Skip(offset).Take(count).ToListAsync();
            return new CollectionResult<T>(totalCount, data);
        }

        public static CollectionResult<T> AsCollection<T>(this IList<T> query, int offset, int count)
        {
            var totalCount = query.Count();
            var data = query.Skip(offset).Take(count).ToList();
            return new CollectionResult<T>(totalCount, data);
        }
    }
}