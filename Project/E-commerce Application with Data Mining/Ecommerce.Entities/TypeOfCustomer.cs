using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Entities
{
    public class TypeOfCustomer
    {
        [Key]
        public int TypeOfCustomer_Id { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string TypeOfCustomer_Description { get; set; }

    }
}
