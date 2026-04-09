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
            
            //新增
            var entity = await userRepository.AddAsync(new User { Username = "wy" });
            var list = new List<User> { new User { Username = "中国" }, new User { Username = "日本" } };
            await userRepository.AddRangeAsync(list);
            //修改
            await userRepository.UpdateEmailAsync(entity.Id, "www@wy.com");
            //获取数据
            var data = await userRepository.GetListAsync();
            //删除数据
            var riben = await userRepository.GetFirstAsync(a => a.Username == "日本");
            await userRepository.DeleteAsync(riben.Id);

            data = await userRepository.GetListAsync();
            return data;
        }
    }
}
