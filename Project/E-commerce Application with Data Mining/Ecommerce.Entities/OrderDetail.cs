using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
   public class OrderDetail
    {
       [Key]
       public int OrderDetailId { get; set; }

       public int OrderId { get; set; }
       [ForeignKey("OrderId")]
       public virtual Order Order { get; set; }

       public int ProductId { get; set; }
       [ForeignKey("ProductId")]
       public virtual Product Product { get; set; }

       public int Size_Id { get; set; }
       [ForeignKey("Size_Id")]
       public virtual Size Size { get; set; }

       public int Quantity { get; set; }
       public decimal UnitPrice { get; set; }
   }
}
