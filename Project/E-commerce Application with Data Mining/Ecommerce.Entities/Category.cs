using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Entities
{
   public class Category
    {
       [Key]
        public int Category_Id { get; set; }
        
        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string Category_Name { get; set; }


        

        //one to many-bir categorinin içinde bir çok subcategory bulunur
        //kadın(category)->etek,pantolon,gömlek...(subcategories)
        //bu yüzden subcategorye referans verilirken ICollectionda tuttuk
        //by Kübra
    }
}
