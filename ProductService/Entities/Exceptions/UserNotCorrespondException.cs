using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class UserNotCorrespondException : BadRequestException
    {
        public UserNotCorrespondException(Guid id) : base("User with id {id} dont have this product")
        { }
    }
}
