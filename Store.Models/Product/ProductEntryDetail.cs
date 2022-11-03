using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Product
{
    public class ProductEntryDetail: ProductEntryDetailDTO
    {
        public int Id { get; set; }
        public int IdEntry { get; set; }
    }
}
