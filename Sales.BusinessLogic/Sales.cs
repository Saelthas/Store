using Common.BusinessLogic;
using Common.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Models;
using Store.Models.Product;
using Store.Models.Sale;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sales.BusinessLogic
{
    public class Sales : ISales
    {
        private ISalesDB _salesDB;
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;
        public Sales(IConfiguration configuration, ILogger<Sales> logger, ISalesDB productsDB, IHttpClientFactory httpClientFactory)
        {
            this._salesDB = productsDB;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public Response GetAllSales()
        {
            var products = _salesDB.GetAllSales();
            if (products.Data == null)
                return Response.Error(products.Message);
            return Response.Success(products.Data);

        }

        public Response GetSale(int Id)
        {
            var data = _salesDB.GetSaleById(Id);
            if (data.Data == null)
                return Response.Error(data.Message);
            var sale = data.Data as Sale;
            var dataDetails = _salesDB.GetDetailSaleById(Id);
            sale.Details = dataDetails.Data as List<SaleDetailDTO>;
            return Response.Success(sale);
        }
        public Response CreateSale(SaleDTO sale)
        {
            Decimal Tax = sale.Subtotal * (Convert.ToDecimal(_configuration["AppSettings:Tax"].ToString()));
            var NewSale = _salesDB.CreateSale(new Sale() { Subtotal=sale.Subtotal-Tax, Tax= Tax, Total= sale.Subtotal});
            if (NewSale.Data == null)
                return Response.Error(NewSale.Message);
            else
            {
                foreach (var Detail in sale.Details)
                {
                    _salesDB.CreateSaleDetail(new SaleDetail()
                    {
                        IdSale = (int)NewSale.Data,
                        IdProduct = Detail.IdProduct,
                        Quantity = Detail.Quantity
                    });
                    //_productsDB.UpdateStockProduct(new Product() { Id = Detail.IdProduct, Stock = Detail.Quantity });
                }
            }
            return Response.Success(NewSale.Data);
        }
        public async Task<Response> UpdateStatusSaleAsync(Sale sale)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["AppSettings:BaseUrlProductsMS"].ToString());
            string UriGetProductById=_configuration["AppSettings:ProductById"].ToString();
            string UriUpdateStockProduct = _configuration["AppSettings:ProductUpdateStock"].ToString();
            List<Product> products = new List<Product>();
            try
            {
                var DetailsData = _salesDB.GetDetailSaleById(sale.Id);
                if(DetailsData.Data==null)
                    return Response.Error($"No se tienen Productos registrados para la venta con id: {sale.Id}");
                var Details = DetailsData.Data as List<SaleDetailDTO>;

                foreach (var detail in Details)
                {
                    var ResponseMessage = await httpClient.GetAsync(UriGetProductById+"/"+detail.IdProduct);
                    string JsonResponse = ResponseMessage.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Response<Product>>(JsonResponse);
                    var product = response.Data;
                    if (product.Stock < detail.Quantity)
                        return Response.Error($"Solo se tienen disponibles {product.Stock} de los {detail.Quantity} solicitados del producto: {product.Name}.");
                    products.Add(product);
                }
                Response taskUptadeStock = await UpdateProductsStock(products, Details);
                var UpSale = _salesDB.UpdateStatusSale(sale);
                if (UpSale.Data == null)
                    return Response.Error(UpSale.Message);
                
                return Response.Success(UpSale.Data);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private async Task<Response> UpdateProductsStock(List<Product> products, List<SaleDetailDTO> details)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["AppSettings:BaseUrlProductsMS"].ToString());
            string UriUpdateStockProduct = _configuration["AppSettings:ProductUpdateStock"].ToString();

            foreach (var product in products)
            {
                SaleDetailDTO detail = details.Find(x => x.IdProduct == product.Id);
                Request<ProductStockDTO> stock = new Request<ProductStockDTO>() { Data= new ProductStockDTO() { stock= detail .Quantity} };
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(stock), null, "application/json");
                var ResponseMessage = await httpClient.PutAsync(UriUpdateStockProduct + "/" + product.Id,httpContent);
                string JsonResponse = ResponseMessage.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Response<int>>(JsonResponse);

            }
            return null;
        }

        public Response DeleteSale(int Id)
        {
            var DelSale = _salesDB.DeleteSale(Id);
            if (DelSale.Data == null)
                return Response.Error(DelSale.Message);
            return Response.Success(DelSale.Data);
        }
    }
}
