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
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(100)]
        public string Product_Description { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal Product_Cost { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(100)]
        public string Product_Warning { get; set; }

        public int Product_SubCategory_Id { get; set; }
        [ForeignKey("Product_SubCategory_Id")]
        public virtual SubCategory SubCategory { get; set; }

        public int Product_Boutique_Id { get; set; }
        [ForeignKey("Product_Boutique_Id")]
        public virtual Boutique ProductBoutiqueId { get; set; }

        /*     [Required, Column(TypeName = "int")]
             public int Product_NumberInStock { get; set; }*/


        public virtual ICollection<Image> Images { get; set; }
        //MANY TO MANY 
        public virtual ICollection<Size> Sizes { get; set; }
        public virtual ICollection<Color> Colors { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }


    }
}
