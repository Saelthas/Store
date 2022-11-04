using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Sale
{
    public class SaleDTO
    {
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public List<SaleDetailDTO> Details { get; set; }
        
        public SaleDTO()
        {
            Details = new List<SaleDetailDTO>();
        }
    }
}
