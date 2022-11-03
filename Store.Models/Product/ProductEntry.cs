using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Product
{
    public class ProductEntry: ProductEntryDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
