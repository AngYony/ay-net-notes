using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DearlerPlatform.Core.Repository
{
    /// <summary>
    /// 大多数情况下不需要使用，如果需要使用，应该传入Expression<Func<TEntity,bool>> predicate)的形式
    /// </summary>
    public static class LinqExtensions
    {
        public async static IAsyncEnumerable<TEntity> Ext_WhereAsync<TEntity>(this DbSet<TEntity> dbSet, Func<TEntity, bool> predicate)
            where TEntity : class
        {
            var res = dbSet.Where(predicate);
            foreach (var item in res)
            {
                yield return item;
            }
        }


        public async static Task<List<TEntity>> Ext_ToListAsync<TEntity>(this IAsyncEnumerable<TEntity> listAsync)
        {
            List<TEntity> list = new();
            await foreach (var item in listAsync) 
            {
                list.Add(item);
            }
            return list;
        }

    }
}
