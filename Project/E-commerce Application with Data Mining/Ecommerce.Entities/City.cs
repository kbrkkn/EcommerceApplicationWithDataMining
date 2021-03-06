﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar"), MaxLength(50)]
        public string Name { get; set; }

    }
}
