using AY.SmartEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<bool> UpdateEmailAsync(int id, string email);
    }
}
