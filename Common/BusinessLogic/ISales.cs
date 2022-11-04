using Store.Models;
using Store.Models.Sale;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusinessLogic
{
    public interface ISales
    {
        Response GetAllSales();
        Response GetSale(int Id);
        Response CreateSale(SaleDTO sale);
        Task<Response> UpdateStatusSaleAsync(Sale sale);
        Response DeleteSale(int Id);
    }
}
