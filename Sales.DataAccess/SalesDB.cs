using Common.DataAccess;
using DataAccess.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using Store.Models;
using Store.Models.Sale;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sales.DataAccess
{
    public class SalesDB : ISalesDB
    {
        private ILogger<SalesDB> _logger;
        private string _connection;
        private IConfiguration _configuration;
        public SalesDB(ILogger<SalesDB> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._connection = Connections.StoreDatabase(configuration);
            this._configuration = configuration;
        }
        public Response GetAllSales()
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[GetAllSales]");
                //storeProcedure.AddParameter("@CLIE_CIC_VC", cic);
                var dataTable = storeProcedure.ExecuteQuery(_connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        List<Sale> products = dataTable.AsEnumerable().Select(m => new Sale()
                        {
                            Id = m.Field<int>("Id"),
                            Subtotal = m.Field<decimal>("Subtotal"),
                            Tax = m.Field<decimal>("Tax"),
                            Total = m.Field<decimal>("Total"),
                            Status = m.Field<string>("Status"),

                            CreatedDate = m.Field<DateTime>("CreatedDate"),
                            UpdatedDate = m.Field<DateTime>("UpdatedDate")
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
        public Response GetSaleById(int id)
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[GetSaleById]");
                storeProcedure.AddParameter("@Id", id);
                var dataTable = storeProcedure.ExecuteQuery(_connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        Sale products = dataTable.AsEnumerable().Select(m => new Sale()
                        {
                            Id = m.Field<int>("Id"),
                            Subtotal = m.Field<decimal>("Subtotal"),
                            Tax = m.Field<decimal>("Tax"),
                            Total = m.Field<decimal>("Total"),
                            Status = m.Field<string>("Status"),
                            CreatedDate = m.Field<DateTime>("CreatedDate"),
                            UpdatedDate = m.Field<DateTime>("UpdatedDate")
                        }).FirstOrDefault();
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
        public Response GetDetailSaleById(int id)
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[GetDetailSaleById]");
                storeProcedure.AddParameter("@idSale", id);
                var dataTable = storeProcedure.ExecuteQuery(_connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        List<SaleDetailDTO> Details = dataTable.AsEnumerable().Select(m => new SaleDetailDTO()
                        {
                            //IdSale = m.Field<int>("IdSale"),
                            Quantity = m.Field<int>("Quantity"),
                            IdProduct = m.Field<int>("IdProduct"),

                        }).ToList();
                        return Response.Success(Details);
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
        public Response CreateSale(Sale product)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[AddSale]");
                storeProcedure.AddParameter("@Subtotal", product.Subtotal);
                storeProcedure.AddParameter("@Tax", product.Tax);
                storeProcedure.AddParameter("@Total", product.Total);

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
        public Response CreateSaleDetail(SaleDetail product)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[AddSaleDetail]");
                storeProcedure.AddParameter("@IdSale", product.IdSale);
                storeProcedure.AddParameter("@Quantity", product.Quantity);
                storeProcedure.AddParameter("@IdProduct", product.IdProduct);

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
        public Response UpdateStatusSale(Sale product)
        {
            try
            {
                StoreProcedure storeProcedure = new StoreProcedure("[dbo].[UpdateStatusSale]");
                storeProcedure.AddParameter("@Id", product.Id);
                storeProcedure.AddParameter("@Status", product.Status);
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

        public Response DeleteSale(int Id)
        {
            try
            {
                var storeProcedure = new StoreProcedure("[dbo].[DeleteSale]");
                storeProcedure.AddParameter("@id", Id);
                var dataTable = storeProcedure.ExecuteQuery(_connection);
                if (storeProcedure.Error.Length <= 0)
                {
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["@@ROWCOUNT"].ToString() != "0")
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
    }
}
