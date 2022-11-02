using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Product
{
    public class Product: ProductDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
