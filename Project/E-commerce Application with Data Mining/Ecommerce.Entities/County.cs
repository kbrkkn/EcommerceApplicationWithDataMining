using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
    public class County
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string Name { get; set; }

        // 1 to N
        public int? City_Id { get; set; }
        [ForeignKey("City_Id")]
        public virtual City City { get; set; }
    }
}
