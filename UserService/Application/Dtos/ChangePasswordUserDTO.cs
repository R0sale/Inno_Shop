using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ChangePasswordUserDTO
    {
        [Required(ErrorMessage = "Email is a required field")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "NewPassword is a required field")]
        public string? NewPassword { get; set; }
    }
}
