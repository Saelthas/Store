using Common.BusinessLogic;
using Common.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Store.Models;
using Store.Models.Product;
using System;

namespace Products.BusinessLogic
{
    public class Products : IProducts
    {
        private IProductsDB _productsDB;
        public Products(IConfiguration configuration, ILogger<Products> logger, IProductsDB productsDB)
        {
            this._productsDB = productsDB;
        }
        public Response GetAllProduct()
        {
            var products = _productsDB.GetAllProducts();
            return Response.Success(products.Data);

        }

        public Response GetProduct(int Id)
        {
            var product = _productsDB.GetProduct(Id);
            if (product.Data == null)
                return Response.Error(product.Message);
            return Response.Success(product.Data);
        }
        public Response CreateProduct(ProductDTO product)
        {
            var NewProduct = _productsDB.AddProduct(product);
            if (NewProduct.Data == null)
                return Response.Error(NewProduct.Message);
            return Response.Success(NewProduct.Data);
        }
        public Response UpdateProduct(ProductDTO product)
        {
            var UpProduct = _productsDB.AddProduct(product);
            if (UpProduct.Data == null)
                return Response.Error(UpProduct.Message);
            return Response.Success(UpProduct.Data);
        }
    }
}
