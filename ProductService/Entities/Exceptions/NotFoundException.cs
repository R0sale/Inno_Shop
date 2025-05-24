using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(Guid id, string item) : base($"{item} with id: {id} does not exist in the db")
        { }
    }
}
