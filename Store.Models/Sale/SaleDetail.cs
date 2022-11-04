using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Sale
{
    public class SaleDetail: SaleDetailDTO
    {
        public int Id { get; set; }
        public int IdSale { get; set; }
    }
}
