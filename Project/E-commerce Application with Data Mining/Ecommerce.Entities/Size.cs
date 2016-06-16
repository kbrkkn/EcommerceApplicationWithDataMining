using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Entities
{
    public class Size
    {
        [Key]
        public int Size_Id { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string Size_Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

    }
}
