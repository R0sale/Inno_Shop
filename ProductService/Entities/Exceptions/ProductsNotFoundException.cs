using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class ProductsNotFoundException : Exception
    {
        public ProductsNotFoundException() : base("There is no products in the database")
        { }
    }
}
