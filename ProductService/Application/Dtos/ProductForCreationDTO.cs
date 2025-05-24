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
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(20, ErrorMessage = "The max length of name field is 20.")]
        [MinLength(5, ErrorMessage = "The min length of name field is 5")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Accessability is a required field.")]
        public bool Accessibility { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        public string Description { get; set; }

        [Range(1, 100000)]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "OwnerId is a required field.")]
        public Guid OwnerId { get; set; }

        [Required(ErrorMessage = "CreationDate is a required field.")]
        public DateTime CreationDate { get; set; }
    }
}
