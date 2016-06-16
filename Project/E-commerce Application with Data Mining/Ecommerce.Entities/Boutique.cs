using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ecommerce.Entities
{
    public class Boutique
    {
        [Key]
        public int Boutique_Id { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(100)]
        public string Boutique_Description { get; set; }
        
       public int Boutique_Stock { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        public byte[] ImageData { get; set; }

        public string FileName { get; set; }

    }
}
