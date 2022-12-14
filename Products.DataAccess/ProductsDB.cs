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
        private ILogger<ProductsDB> _logger;
        private string _connection;
        private IConfiguration _configuration;
        public ProductsDB(ILogger<ProductsDB> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connection = Connections.StoreDatabase(configuration);
            _configuration = configuration;
        }
        public Response GetAllProducts()
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[GetAllProducts]");
                //storeProcedure.AddParameter("@CLIE_CIC_VC", cic);
                var dataTable = storeProcedure.ExecuteQuery(_connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        List<Product> products = dataTable.AsEnumerable().Select(m => new Product()
                        {
                            Id = m.Field<int>("Id"),
                            Code = m.Field<string>("Code"),
                            Name = m.Field<string>("Name"),
                            Stock = m.Field<int>("Stock"),
                            Price = m.Field<decimal>("Price"),
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
                    this._logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Response GetProduct(int Id)
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[GetProduct]");
                storeProcedure.AddParameter("@id", Id);
                var dataTable = storeProcedure.ExecuteQuery(_connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        Product products = dataTable.AsEnumerable().Select(m => new Product()
                        {
                            Id = m.Field<int>("Id"),
                            Code = m.Field<string>("Code"),
                            Name = m.Field<string>("Name"),
                            Stock = m.Field<int>("Stock"),
                            Price = m.Field<decimal>("Price"),
                            Description = m.Field<string>("Description"),

                            CreatedDate = m.Field<DateTime>("CreatedDate"),
                            UpdateDate = m.Field<DateTime>("UpdateDate")
                        }).First();
                        return Response.Success(products);
                    }
                    else
                        return Response.Error($"El Identificador {Id} de Producto no existe.");
                }
                else
                {
                    this._logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
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
                storeProcedure.AddParameter("@Code", product.Code);
                storeProcedure.AddParameter("@Price", product.Price);
                DataTable dataTable = storeProcedure.ExecuteQuery(_connection);

                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        int identity = 0;
                        foreach (DataRow item in dataTable.Rows)
                        {
                            identity = Convert.ToInt32(item["@@IDENTITY"].ToString());
                        }
                        return Response.Success(identity);
                    }
                    else
                        return (Response.Error("No se pudo crear el producto"));
                }
                else
                {
                    this._logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public Response UpdateProduct(Product product)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[UpdateProduct]");
                storeProcedure.AddParameter("@Id", product.Id);
                storeProcedure.AddParameter("@Code", product.Code);
                storeProcedure.AddParameter("@Name", product.Name);
                storeProcedure.AddParameter("@Description", product.Description);
                //storeProcedure.AddParameter("@Code", product.Description);
                storeProcedure.AddParameter("@Price", product.Price);
                DataTable dataTable = storeProcedure.ExecuteQuery(_connection);

                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        int identity = 0;
                        foreach (DataRow item in dataTable.Rows)
                        {
                            identity = Convert.ToInt32(item["@@ROWCOUNT"].ToString());
                        }
                        return Response.Success(identity);
                    }
                    else
                        return (Response.Error("No se pudo Actualizar el producto"));
                }
                else
                {
                    this._logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Response DeleteProduct(int Id)
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[DeleteProduct]");
                storeProcedure.AddParameter("@id", Id);
                var dataTable = storeProcedure.ExecuteQuery(_connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0&& dataTable.Rows[0]["@@ROWCOUNT"].ToString()!="0")
                    {
                        return Response.Success(Convert.ToInt32(dataTable.Rows[0]["@@ROWCOUNT"].ToString()));
                    }
                    else
                        return Response.Error("No se pudo realizar la eliminacion");
                }
                else
                {
                    this._logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Response RegisterEntry(ProductEntryDTO entry)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[CreateEntry]");
                storeProcedure.AddParameter("@Supplier", entry.Supplier);
                DataTable dataTable = storeProcedure.ExecuteQuery(_connection);

                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        int identity = 0;
                        foreach (DataRow item in dataTable.Rows)
                        {
                            identity = Convert.ToInt32(item["@@IDENTITY"].ToString());
                        }
                        return Response.Success(identity);
                    }
                    else
                        return (Response.Error("No se pudo crear el producto"));
                }
                else
                {
                    this._logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public Response RegisterDetailEntry(ProductEntryDetail entry)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[CreateDetailEntry]");
                storeProcedure.AddParameter("@IdEntry", entry.IdEntry);
                storeProcedure.AddParameter("@Quantity", entry.Quantity);
                storeProcedure.AddParameter("@IdProduct", entry.IdProduct);
                DataTable dataTable = storeProcedure.ExecuteQuery(_connection);

                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        int identity = 0;
                        foreach (DataRow item in dataTable.Rows)
                        {
                            identity = Convert.ToInt32(item["@@IDENTITY"].ToString());
                        }
                        return Response.Success(identity);
                    }
                    else
                        return (Response.Error("No se pudo crear el producto"));
                }
                else
                {
                    this._logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public Response UpdateStockProduct(Product product)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[UpdateStockProduct]");
                storeProcedure.AddParameter("@Id", product.Id);
                storeProcedure.AddParameter("@Stock", product.Stock);
                DataTable dataTable = storeProcedure.ExecuteQuery(_connection);

                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        int identity = 0;
                        foreach (DataRow item in dataTable.Rows)
                        {
                            identity = Convert.ToInt32(item["@@ROWCOUNT"].ToString());
                        }
                        return Response.Success(identity);
                    }
                    else
                        return (Response.Error("No se pudo Actualizar el producto"));
                }
                else
                {
                    this._logger?.LogError(storeProcedure.Error);
                    return Response.ExceptionGenerate(storeProcedure.Error, Validation.SuggestedMessages.ErrorSql);
                }
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
