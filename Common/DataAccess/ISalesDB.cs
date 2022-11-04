using Store.Models;
using Store.Models.Sale;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DataAccess
{
    public interface ISalesDB
    {
        Response GetAllSales();
        Response GetSaleById(int id);
        Response GetDetailSaleById(int id);
        Response CreateSale(Sale product);
        Response CreateSaleDetail(SaleDetail product);
        Response UpdateStatusSale(Sale product);
        Response DeleteSale(int Id);
    }
}
