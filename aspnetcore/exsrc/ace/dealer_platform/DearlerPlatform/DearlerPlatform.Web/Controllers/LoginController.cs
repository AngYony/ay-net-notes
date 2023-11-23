using DearlerPlatform.Common.Md5Module;
using DearlerPlatform.Common.TokenModule;
using DearlerPlatform.Service.CustomerApp;
using DearlerPlatform.Service.CustomerApp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        public LoginController(
            ICustomerService customerService,
            IConfiguration configuration)
        {
            CustomerService = customerService;
            Configuration = configuration;
        }

        public ICustomerService CustomerService { get; }
        public IConfiguration Configuration { get; }


        [HttpPost]
        public async Task<string> CheckLoginAsync(CustomerLoginDto dto)
        {
            if(string.IsNullOrWhiteSpace(dto.CustomerNo) || string.IsNullOrWhiteSpace(dto.Password))
            {
                HttpContext.Response.StatusCode = 400;
                return "NonLoginInfo";
            }

            var isSucess = await CustomerService.CheckPasswordAsync(dto);
            if (isSucess)
            {
                var customer = await CustomerService.GetCustomerAsync(dto.CustomerNo);
                return GetToken(customer.Id, customer.CustomerNo, customer.CustomerName);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
                return "NonUser";
            }
        }



        [HttpGet("GetMd5")]
        public string GetMd5(string str)
        {
            return "111".ToMd5();
        }

        private string GetToken(int customerId, string customerNo, string customerName)
        {
            var token= Configuration.GetSection("Jwt").Get<JwtTokenModel>();
            token.Id = customerId;
            token.CustomerNo = customerNo;
            token.CustomerName = customerName;
            return TokenHelper.CreateToken(token);

        }
    }
}
