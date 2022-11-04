using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Sale
{
    public class Sale: SaleDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
