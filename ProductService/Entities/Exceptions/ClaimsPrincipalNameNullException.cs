using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ClaimsPrincipalNameNullException : BadRequestException
    {
        public ClaimsPrincipalNameNullException() : base("UserName of ClaimsPrincipal is null")
        { }
    }
}
