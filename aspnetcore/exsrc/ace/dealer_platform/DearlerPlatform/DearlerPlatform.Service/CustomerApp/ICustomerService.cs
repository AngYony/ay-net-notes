using DearlerPlatform.Domain;
using DearlerPlatform.Service.CustomerApp.Dto;
using System.Threading.Tasks;

namespace DearlerPlatform.Service.CustomerApp
{
    public interface ICustomerService:IocTag
    {
        Task<bool> CheckPasswordAsync(CustomerLoginDto dto);

        Task<Customer> GetCustomerAsync(string customerNo);
    }
}