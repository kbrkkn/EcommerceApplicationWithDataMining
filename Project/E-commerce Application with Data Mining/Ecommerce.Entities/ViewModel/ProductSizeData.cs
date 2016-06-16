using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.ViewModel
{
    public class ProductSizeData
    {

        public IEnumerable<Size> ProductSizes { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Stock> ProductStocks { get; set; }


    }
}
