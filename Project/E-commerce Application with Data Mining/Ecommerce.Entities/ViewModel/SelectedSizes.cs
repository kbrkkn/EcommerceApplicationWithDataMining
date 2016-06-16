using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.ViewModel
{
    public class SelectedSizes
    {
        public bool IsSelected { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public int NumberOfstock { get; set; }
        public int Product_Id { get; set; }
    }
}
