using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class User
    {
        public string Id { get; set; }

        [Required,MinLength(2,ErrorMessage = "Minimum Lenght 2")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required,EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [DataType(DataType.Password),Required,MinLength(4,ErrorMessage = "Minimum Lenght 4")]
        public string Password { get; set; }
    }
}
