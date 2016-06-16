using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Entities
{
    public class Color
    {
        [Key]
        public int Color_Id { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string Color_Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
