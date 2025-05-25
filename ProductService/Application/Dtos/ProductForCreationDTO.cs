using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductForCreationDTO
    {
        public string Name { get; set; }

        public bool Accessibility { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
