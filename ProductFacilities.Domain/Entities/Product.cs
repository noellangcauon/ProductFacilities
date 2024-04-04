using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFacilities.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
