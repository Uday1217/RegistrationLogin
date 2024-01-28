using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace RegistrationLogin.Mvc.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [NotMapped]
        public int CategoryId { get; set; }
        public string Category { get; set; }

        [NotMapped]
       public int SubCategoryId { get; set; }
       public string SubCategory { get; set; }

       
        [Required]
        public byte[]? Image { get; set; }

        [Required]
        public double Price { get; set; }

    }
}
