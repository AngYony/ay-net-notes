using AutoMapper;
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
            IRepository<ProductSaleAreaDiff> diffRepo,
            IMapper mapper
            )
        {
            ProductRepo = productRepo;
            ProductSaleRepo = productSaleRepo;
            ProductPhotoRepo = productPhotoRepo;
            DiffRepo = diffRepo;
            Mapper = mapper;
        }

        public IRepository<Product> ProductRepo { get; }
        public IRepository<ProductSale> ProductSaleRepo { get; }
        public IRepository<ProductPhoto> ProductPhotoRepo { get; }
        public IRepository<ProductSaleAreaDiff> DiffRepo { get; }
        public IMapper Mapper { get; }



        public async Task<IEnumerable<ProductDto>> GetProductDto(string sort, int pageIndex, int pageSize)
        {
            var products = await ProductRepo.GetListAsync(a => a.Id, pageIndex, pageSize);
            var dtos = Mapper.Map<List<ProductDto>>(products);

            var productPhotos = await GetProductPhotosByProductNo(products.Select(m => m.ProductNo).ToArray());
            var productSales = await GetProductSalesByProductNo(products.Select(m => m.ProductNo).ToArray());
            dtos.ForEach(p =>
            {
                p.ProductPhoto = productPhotos.FirstOrDefault(m => m.ProductNo == p.ProductNo);
                p.ProductSale = productSales.FirstOrDefault(m => m.ProductNo == p.ProductNo);
                // var productSale = productSales.FirstOrDefault(m=>m.ProductNo == p.ProductNo);
            });
            return dtos;
        }


    }
}
