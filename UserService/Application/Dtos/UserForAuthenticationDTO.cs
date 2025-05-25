using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserForAuthenticationDTO
    {
        public string? UserName { get; init; }
        public string? Password { get; init; }
    }
}
