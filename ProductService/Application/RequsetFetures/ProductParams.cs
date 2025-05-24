using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestFeatures
{
    public class ProductParams : RequestParams
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; } = int.MaxValue;
        public DateTime MinCreationDate { get; set; }
        public DateTime MaxCreationDate { get; set; } = DateTime.Now;

        public string? Searchterm { get; set; }

        public bool ValidPrice => MaxPrice > MinPrice;
        public bool ValidDate => MaxCreationDate > MinCreationDate;
    }
}
