using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.ViewModel
{
    public class ProductColorData
    {
        public IEnumerable<Color> ProductColors { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
