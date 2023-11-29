using DearlerPlatform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Service.ProductApp.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string TypeNo { get; set; }
        public string TypeName { get; set; }

        public ProductPhoto ProductPhoto { get; set; }
        public ProductSale ProductSale { get; set; }

    }
}
