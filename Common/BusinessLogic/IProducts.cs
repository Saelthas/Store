using Store.Models;
using Store.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusinessLogic
{
    public interface IProducts
    {
        Response GetAllProduct();
        Response GetProduct(int Id);
        Response CreateProduct(ProductDTO product);
        Response UpdateProduct(Product product);
        Response DeleteProduct(int Id);
    }
}
