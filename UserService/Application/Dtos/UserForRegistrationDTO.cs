using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserForRegistrationDTO
    {
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(20, ErrorMessage = "The max length of name field is 20.")]
        [MinLength(5, ErrorMessage = "The min length of name field is 5")]
        public string? FirstName { get; init; }

        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(20, ErrorMessage = "The max length of name field is 20.")]
        [MinLength(5, ErrorMessage = "The min length of name field is 5")]
        public string? LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "The max length of name field is 20.")]
        [MinLength(5, ErrorMessage = "The min length of name field is 5")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Roles is a required field.")]
        public ICollection<string>? Roles { get; init; }
    }
}
