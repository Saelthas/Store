using Common.BusinessLogic;
using Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.DataAccess;
using Store.Models;
using Store.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;
        //private readonly DBContextInMemmory _context;
        private IProducts _products;

        public ProductsController(ILogger<ProductsController> logger, IProducts products) : base(logger)
        {
            _logger = logger;
            //_context = context;
            this._products = products;
        }
        //[Route("getAllProducts")]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Execute(_products.GetAllProduct);
        }
        //[Route("getProductById/{id}")]
        [HttpGet("{id}", Name = "ProductById")]
        public IActionResult GetProductById(int id)
        {
            return Execute<int, Response>(new Request<int>() { Data = id }, _products.GetProduct);
            //return null;
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Request<ProductDTO> request)
        {

            if (request == null)
            {
                return BadRequest("Owner object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            return Execute<ProductDTO, Int64>(request, _products.CreateProduct);
            //var respo1 = ((response as ObjectResult).Value) as Response<Int64>;
            //var cast1 = respo1 as Response<Int64>;
            //int id = (int)((Response<Int64>)(response as ObjectResult).Value  ).Data;
            //return CreatedAtRoute("ProductById", new { id = respo1.Data }, response);


        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Request<ProductDTO> request)
        {
            if (id == 0)
            {
                return BadRequest("Id invalido");
            }
            if (request == null)
            {
                return BadRequest("Owner object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            return Execute<ProductDTO, Int64>(request, _products.CreateProduct);
            return null;
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {

            return null;
        }
    }
}
