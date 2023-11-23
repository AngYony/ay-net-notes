using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.ProductApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Service.ProductApp
{
    public partial class ProductService : IProductService
    {
        public ProductService(
            IRepository<Product> productRepo,
            IRepository<ProductSale> productSaleRepo,
            IRepository<ProductPhoto> productPhotoRepo,
            IRepository<ProductSaleAreaDiff> diffRepo
            )
        {
            ProductRepo = productRepo;
            ProductSaleRepo = productSaleRepo;
            ProductPhotoRepo = productPhotoRepo;
            DiffRepo = diffRepo;
        }

        public IRepository<Product> ProductRepo { get; }
        public IRepository<ProductSale> ProductSaleRepo { get; }
        public IRepository<ProductPhoto> ProductPhotoRepo { get; }
        public IRepository<ProductSaleAreaDiff> DiffRepo { get; }



        public async Task<IEnumerable<ProductDto>> GetProductDto(string sort, int pageIndex, int pageSize)
        {
            var products = await ProductRepo.GetListAsync(a => a.Id, pageIndex, pageSize);

        }


    }
}
