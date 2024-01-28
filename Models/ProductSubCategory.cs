﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationLogin.Mvc.Models
{
    public class ProductSubCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("ProductCategory")]
        public int CategoryId { get; set; }
       // public ProductCategory Category { get; set; }
    }
}
