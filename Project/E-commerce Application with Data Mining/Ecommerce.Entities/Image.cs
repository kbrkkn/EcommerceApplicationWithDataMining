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
    public class Image
    {
        [Key]
        public int Image_Id { get; set; }
        
        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        public byte[] ImageData { get; set; }

        public string FileName { get; set; }

        // 1 to N
        public int? Product_Id { get; set; }
        [ForeignKey("Product_Id")]
        public virtual Product Product { get; set; }
    }
}
