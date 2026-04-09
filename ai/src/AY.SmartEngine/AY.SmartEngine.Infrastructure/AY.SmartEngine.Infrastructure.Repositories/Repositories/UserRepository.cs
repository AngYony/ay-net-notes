using AY.SmartEngine.Domain.Entities;
using AY.SmartEngine.Domain.Repositories;
using AY.SmartEngine.Infrastructure.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Infrastructure.Repositories.Repositories
{
    public class UserRepository : RepositoryBase<User, LearningTagDbContext>, IUserRepository
    {
        public UserRepository(IDbContextFactory<LearningTagDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }
        public async Task AddAsync(User user)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
