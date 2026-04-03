using AY.SmartEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.ApplicatonServices.Users
{
    public interface IUserService
    {
        Task<List<User>> GetUserAsync();
    }
}
