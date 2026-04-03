using AY.SmartEngine.Domain.Entities;
using AY.SmartEngine.Domain.Repositories;
using AY.SmartEngine.Infrastructure.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Infrastructure.Repositories.Repositories
{
    public class UserRepository(IDbContextFactory<LearningTagDbContext> dbContextFactory) : IUserRepository
    {
        public async Task<List<User>> GetAllAsync()
        {
            // 使用工厂模式支持多数据库连接或多线程访问
            using var context = await dbContextFactory.CreateDbContextAsync();
            return await context.Users.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
