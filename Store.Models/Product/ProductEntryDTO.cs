using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Product
{
    public class ProductEntryDTO
    {
        public string Supplier { get; set; }
        public List<ProductEntryDetailDTO> ProductEntryDetailDTOs { get; set; }
        public ProductEntryDTO()
        {
            ProductEntryDetailDTOs = new List<ProductEntryDetailDTO>();
        }
    }
}
