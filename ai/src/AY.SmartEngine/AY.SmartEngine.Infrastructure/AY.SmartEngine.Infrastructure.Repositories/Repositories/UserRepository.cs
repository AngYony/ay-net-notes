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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEmailAsync(int id, string email)
        {
            return await base.UpdateAsync(x => x.Id == id, s => s.SetProperty(x => x.Email, email)) > 0;
        }
    }
}
