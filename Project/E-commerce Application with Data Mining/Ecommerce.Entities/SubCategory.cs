using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Entities
{
   public class SubCategory
    {/////
        //primary keyi kendimiz oluşturmak istersek bunu kullanırız,burda kullanmadık.
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SubCategory_Id { get; set; }


        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string SubCategory_Name { get; set; }


        public int? Category_Id { get; set; } 
        [ForeignKey("Category_Id")]
        public virtual Category Category { get; set; }


        
        //subcategorynin içinde categorye referans verdik.
        
    }
}
