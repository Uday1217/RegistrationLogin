using MessagePack;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace RegistrationLogin.Mvc.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = ("Can't Empty"))]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = ("Can't Empty"))]
        public string LastName { get; set; }


        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = ("Can't Empty"))]
        public string Email { get; set; }


        [Required]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])" +
            "(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = ("Can't Empty"))]
        [Compare("Password")]
        public string Confirm_Password { get; set; }
    }
}
