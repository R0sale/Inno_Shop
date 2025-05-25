using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ClaimsPrincipalIdNullException : BadRequestException
    {
        public ClaimsPrincipalIdNullException() : base("Id of ClaimsPrincipal is null")
        { }
    }
}
