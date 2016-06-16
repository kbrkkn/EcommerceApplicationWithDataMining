using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
   public class Stock
    {
        [Key]
        public int Id { get; set; }

        public int Size_Id { get; set; }
        [ForeignKey("Size_Id")]
        public virtual Size Size { get; set; }

        public int Product_Id { get; set; }
        [ForeignKey("Product_Id")]
        public virtual Product Product { get; set; }

       public int NumberOfStock { get; set; }

  
   }
}
