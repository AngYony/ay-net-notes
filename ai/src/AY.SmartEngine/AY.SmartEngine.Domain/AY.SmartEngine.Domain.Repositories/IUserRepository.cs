using AY.SmartEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
    }
}
