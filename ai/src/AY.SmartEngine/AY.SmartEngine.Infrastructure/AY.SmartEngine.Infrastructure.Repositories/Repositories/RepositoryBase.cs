using AY.SmartEngine.Domain.Entities;
using AY.SmartEngine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Infrastructure.Repositories.Repositories
{
    public class RepositoryBase<TEntity, TDbContext> : IRepositoryBase<TEntity>
        where TEntity : BaseEntity
        where TDbContext : DbContext
    {
        protected readonly IDbContextFactory<TDbContext> _dbContextFactory;

        public RepositoryBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            // 使用工厂模式支持多数据库连接或多线程访问
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
    }
}
