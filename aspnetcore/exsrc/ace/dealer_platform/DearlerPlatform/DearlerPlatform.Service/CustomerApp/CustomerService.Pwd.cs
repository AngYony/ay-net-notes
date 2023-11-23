using DearlerPlatform.Common.Md5Module;
using DearlerPlatform.Service.CustomerApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Service.CustomerApp
{
    public partial class CustomerService
    {
        public async Task<bool> CheckPasswordAsync(CustomerLoginDto dto)
        {
            var res = await CustomerPwdRepo.GetAsync(a => a.CustomerNo == dto.CustomerNo && a.Password == dto.Password.ToMd5());
            return res != null;
        }
    }
}
