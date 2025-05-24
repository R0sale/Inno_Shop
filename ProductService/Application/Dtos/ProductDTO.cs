using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Accessibility { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
