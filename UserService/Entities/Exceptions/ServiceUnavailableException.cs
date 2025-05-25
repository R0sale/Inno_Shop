using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException() : base("Can't connect to another service")
        { }
    }
}
