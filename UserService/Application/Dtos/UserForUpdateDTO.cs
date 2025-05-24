using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserForUpdateDTO
    {
        [Required(ErrorMessage = "FirstName is a required field.")]
        [MaxLength(20, ErrorMessage = "The max length of name field is 20.")]
        [MinLength(5, ErrorMessage = "The min length of name field is 5")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        [MaxLength(20, ErrorMessage = "The max length of name field is 20.")]
        [MinLength(5, ErrorMessage = "The min length of name field is 5")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "UserName is a required field.")]
        [MaxLength(20, ErrorMessage = "The max length of name field is 20.")]
        [MinLength(5, ErrorMessage = "The min length of name field is 5")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Roles is a required field.")]
        public ICollection<string>? Roles { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string? Email { get; set; }
    }
}
