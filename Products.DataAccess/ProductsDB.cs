using Common.DataAccess;
using DataAccess.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using Store.Models;
using Store.Models.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Products.DataAccess
{
    public class ProductsDB : IProductsDB
    {
        private ILogger<ProductsDB> logger;
        private string connection;
        private IConfiguration configuration;
        public ProductsDB(ILogger<ProductsDB> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.connection = Connections.StoreDatabase(configuration);
            this.configuration = configuration;
        }
        public Response GetAllProducts()
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[GetAllProducts]");
                //storeProcedure.AddParameter("@CLIE_CIC_VC", cic);
                var dataTable = storeProcedure.ExecuteQuery(connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        List<Product> products = dataTable.AsEnumerable().Select(m => new Product()
                        {
                            Id = m.Field<int>("Id"),
                            Name = m.Field<string>("Name"),
                            Description = m.Field<string>("Description"),
                            CreatedDate = m.Field<DateTime>("CreatedDate"),
                            UpdateDate = m.Field<DateTime>("UpdateDate")
                        }).ToList();
                        return Response.Success(products);
                    }
                    else
                        return Response.Error("Sin productos registrados.");
                }
                else
                {
                    this.logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Response GetProduct(int Id)
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[GetProduct]");
                storeProcedure.AddParameter("@id", Id);
                var dataTable = storeProcedure.ExecuteQuery(connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        Product products = dataTable.AsEnumerable().Select(m => new Product()
                        {
                            Id = m.Field<int>("Id"),
                            Name = m.Field<string>("Name"),
                            Description = m.Field<string>("Description"),
                            CreatedDate = m.Field<DateTime>("CreatedDate"),
                            UpdateDate = m.Field<DateTime>("UpdateDate")
                        }).First();
                        return Response.Success(products);
                    }
                    else
                        return Response.Error("El Producto no esta registrado");
                }
                else
                {
                    this.logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public Response AddProduct(ProductDTO product)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[AddProduct]");
                storeProcedure.AddParameter("@Name", product.Name);
                storeProcedure.AddParameter("@Description", product.Description);
                DataTable dataTable = storeProcedure.ExecuteQuery(connection);

                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        Int64 identity = 0;
                        foreach (DataRow item in dataTable.Rows)
                        {
                            identity = Convert.ToInt64(item["@@IDENTITY"].ToString());
                        }
                        return Response.Success(identity);
                    }
                    else
                        return (Response.Error("No se pudo crear el producto"));
                }
                else
                {
                    this.logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public Response UpdateProduct(Product product)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[AddProduct]");
                storeProcedure.AddParameter("@Id", product.Id);
                storeProcedure.AddParameter("@Name", product.Name);
                storeProcedure.AddParameter("@Description", product.Description);
                DataTable dataTable = storeProcedure.ExecuteQuery(connection);

                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        Int64 identity = 0;
                        foreach (DataRow item in dataTable.Rows)
                        {
                            identity = Convert.ToInt64(item["@@ROW_COUNT"].ToString());
                        }
                        return Response.Success(identity);
                    }
                    else
                        return (Response.Error("No se pudo crear el producto"));
                }
                else
                {
                    this.logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
