using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Column("ProductId")]
        [Required(ErrorMessage = "Id is a required property")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is a required property")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Accessability is a required property")]
        public bool Accessibility { get; set; }
        [Required(ErrorMessage = "Description is a required field.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is a required property")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "OwnerId is a required property")]
        public Guid OwnerId { get; set; }
        [Required(ErrorMessage = "Creation date is a required property")]
        public DateTime CreationDate { get; set; }
    }
}
