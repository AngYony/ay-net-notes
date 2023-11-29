using AutoMapper;
using DearlerPlatform.Domain;
using DearlerPlatform.Service.ProductApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Service
{
    public class DearlerPlatformProfile : Profile
    {
        public DearlerPlatformProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            //忽略ID字典的映射
            CreateMap<ProductSale, ProductDto>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<ProductPhoto, ProductDto>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<ProductSaleAreaDiff, ProductDto>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
