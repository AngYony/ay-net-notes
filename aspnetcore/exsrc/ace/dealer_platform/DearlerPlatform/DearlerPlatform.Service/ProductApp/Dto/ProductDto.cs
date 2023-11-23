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

        public string ProductPhotoUrl { get; set; }

        public string StockNo { get; set; }
        public double DiffPrice { get; set; }
        public double SalePrice { get; set; }

    }
}
