using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Domain
{
    public  class ProductPhoto : BaseEntity
    {
        public new int Id { get; set; }
        public string  ProductNo { get; set; }
        public string  ProductPhotoUrl { get; set; }
    }
}
