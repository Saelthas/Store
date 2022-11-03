using Store.Models;
using Store.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DataAccess
{
    public interface IProductsDB
    {
        Response GetAllProducts();
        Response GetProduct(int Id);
        Response AddProduct(ProductDTO product);
        Response UpdateProduct(Product product);
        Response DeleteProduct(int Id);
        Response RegisterEntry(ProductEntryDTO entry);
        Response RegisterDetailEntry(ProductEntryDetail entry);


    }
}
