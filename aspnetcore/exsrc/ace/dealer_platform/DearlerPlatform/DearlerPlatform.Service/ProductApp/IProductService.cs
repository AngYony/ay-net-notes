using DearlerPlatform.Domain;
using DearlerPlatform.Service.ProductApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Service.ProductApp
{
    public interface IProductService:IocTag
    {
        Task<IEnumerable<ProductDto>> GetProductDto(string sort, int pageIndex, int pageSize);
    }
}
