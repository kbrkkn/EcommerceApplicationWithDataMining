using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Ecommerce.Entities
{
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string Customer_Name { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string Customer_Surname { get; set; }

        [Required, Column(TypeName = "varchar"), MaxLength(100)]
        public string Customer_Email { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(250)]
        public string Customer_Password { get; set; }

        [Required, Column(TypeName = "bit")]
        public bool Customer_Gender { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime Customer_BirthDate { get; set; }

        [Required, Column(TypeName = "bit")]
        public bool Customer_Contract { get; set; }

        public int Customer_County_Id { get; set; }       
        [ForeignKey("Customer_County_Id")]
        public virtual County CustomerCounty { get; set; }

        [Column(TypeName = "nvarchar"), MaxLength(50)]
        public string ActivationCode { get; set; }

        public string Customer_Address{ get; set; }

        public int CustomerTypeOfCustomerId { get; set; }
        [ForeignKey("CustomerTypeOfCustomerId")]
        public virtual TypeOfCustomer CustomerTypeOfCustomer { get; set; }

        public DateTime AddDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }




    }
}
