using DearlerPlatform.Core.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DearlerPlatform.Core.Repository
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class

    {
        private readonly DearlerPlarformDbContext _context;

        public Repository(DearlerPlarformDbContext dearlerPlarformDbContext)
        {
            this._context = dearlerPlarformDbContext;
        }

        public List<TEntity> GetList()
        {
            //通过泛型获取DbSet，等同于_context.TEntity
            return _context.Set<TEntity>().ToList();
        }

        public List<TEntity> GetList(Func<TEntity, bool> predicate)
        {
            //通过泛型获取DbSet，等同于_context.TEntity
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            //通过泛型获取DbSet，等同于_context.TEntity
            return await _context.Set<TEntity>().ToListAsync();
        }

        ///不建议下述方式定义
        //public async Task<List<TEntity>> GetListAsync(Func<TEntity, bool> predicate)
        //{
        //    //通过泛型获取DbSet，等同于_context.TEntity 
        //    return await _context.Set<TEntity>()
        //        //.Where(predicate).AsQueryable() //将 IEnumerable 转换为 IQueryable。
        //        .Ext_WhereAsync(predicate)   //调用自定义扩展方法
        //        .Ext_ToListAsync();
        //}


        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }


        public TEntity Get(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            //从表达式树中获取委托对象
            //var func = predicate.Compile();

            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public TEntity Insert(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            var res = dbSet.Add(entity).Entity;
            _context.SaveChanges();
            return res;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            var res = (await dbSet.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return res;
        }


        public TEntity Delete(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            var res = dbSet.Remove(entity).Entity;
            _context.SaveChanges();
            return res;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            var res = dbSet.Remove(entity).Entity;
            await _context.SaveChangesAsync();
            return res;
        }


        public TEntity Update(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            var res = dbSet.Update(entity).Entity;
            _context.SaveChanges();
            return res;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            var res = dbSet.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task< IList<TEntity>> GetListAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector, int pageIndex, int pageSize)
        {
            int skipNum = (pageIndex - 1) * pageSize;
            return await _context.Set<TEntity>().OrderBy(keySelector) .Skip(skipNum).Take(pageSize).ToListAsync();
        }
    }
}