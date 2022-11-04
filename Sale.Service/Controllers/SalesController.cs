using Common.BusinessLogic;
using Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Models;
using Store.Models.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {

        private readonly ILogger<SalesController> _logger;
        private ISales _sales;

        public SalesController(ILogger<SalesController> logger, ISales sales) : base(logger)
        {
            _logger = logger;
            _sales = sales;
        }

        [HttpGet]
        public IActionResult GetAllSales()
        {
            return Execute<List<Sale>>(_sales.GetAllSales);
        }
        [HttpGet("{id}")]
        public IActionResult GetSaleById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id invalido");
            }
            return Execute<int, Sale>(new Request<int>() { Data = id }, _sales.GetSale);
        }
        [HttpPost]
        public IActionResult CreateSale([FromBody] Request<SaleDTO> request)
        {
            if (request.Data == null)
            {
                return BadRequest("Objeto ProductDTO es nulo");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo de objecto invalido");
            }
            return Execute<SaleDTO, int>(request, _sales.CreateSale);
        }
        //[Route("DeliveredSale/{id}")]
        [HttpPut("CompleteSale/{id}")]
        public async Task<IActionResult> UpdateStatusSaleToComplete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id invalido");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var x = new Request<Sale> { Data = new Sale() { Id = id, Status = "E" } };
            return await ExecuteAsync<Sale, int>(x, _sales.UpdateStatusSaleAsync);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            if (id == 0)
                return BadRequest("Id invalido");
            return Execute<int, object>(new Request<int>() { Data = id }, _sales.DeleteSale);
        }

    }
}
