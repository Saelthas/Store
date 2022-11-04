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
            if (products.Data == null)
                return Response.Error(products.Message);
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
        public Response UpdateProduct(Product product)
        {
            var UpProduct = _productsDB.UpdateProduct(product);
            if (UpProduct.Data == null)
                return Response.Error(UpProduct.Message);
            return Response.Success(UpProduct.Data);
        }
        public Response UpdateStockProduct(Product product)
        {
            var UpProduct = _productsDB.UpdateStockProduct(product);
            if (UpProduct.Data == null)
                return Response.Error(UpProduct.Message);
            return Response.Success(UpProduct.Data);
        }

        public Response DeleteProduct(int Id)
        {
            var UpProduct = _productsDB.DeleteProduct(Id);
            if (UpProduct.Data == null)
                return Response.Error(UpProduct.Message);
            return Response.Success(UpProduct.Data);
        }
        public Response RegisterEntry(ProductEntryDTO Entry)
        {

            var NewEntry = _productsDB.RegisterEntry(Entry);

            if (NewEntry.Data == null)
                return Response.Error(NewEntry.Message);
            else
            {
                foreach (var Detail in Entry.ProductEntryDetailDTOs)
                {
                    _productsDB.RegisterDetailEntry(new ProductEntryDetail()
                    {
                        IdEntry = (int)NewEntry.Data,
                        IdProduct = Detail.IdProduct,
                        Quantity = Detail.Quantity
                    });
                    _productsDB.UpdateStockProduct(new Product() { Id = Detail.IdProduct, Stock = Detail.Quantity });
                }
            }
            return Response.Success(NewEntry.Data);
        }
    }
}
