using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Product
{
    public class ProductDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
