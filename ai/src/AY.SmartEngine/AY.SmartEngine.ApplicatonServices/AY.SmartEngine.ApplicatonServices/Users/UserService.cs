using AY.SmartEngine.Domain.Entities;
using AY.SmartEngine.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.ApplicatonServices.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<User>> GetUserAsync()
        {
            var data = await userRepository.GetAllAsync();
            return data;
        }
    }
}
