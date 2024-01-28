using System.ComponentModel.DataAnnotations;

namespace RegistrationLogin.Mvc.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
