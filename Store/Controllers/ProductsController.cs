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
            this._products = products;
        }
        /// <summary>
        /// Get all products registered in the DataBase
        /// </summary>
        /// <returns>List of Products</returns>
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Execute<List<Product>>(_products.GetAllProduct);
        }
        /// <summary>
        /// Get a specific product
        /// </summary>
        /// <param name="id">identifier of the product to search</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "ProductById")]
        public IActionResult GetProductById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id invalido");
            }
            return Execute<int, Product>(new Request<int>() { Data = id }, _products.GetProduct);
            //return null;
        }
        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="request">ProductDTO, contains the necesary data for insert to register in the DB</param>
        /// <returns> id of the record inserted.</returns>
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Request<ProductDTO> request)
        {

            if (request.Data == null)
            {
                return BadRequest("Objeto ProductDTO es nulo");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo de objecto invalido");
            }
            return Execute<ProductDTO, int>(request, _products.CreateProduct);
        }
        /// <summary>
        /// Updates all parameters of a specific product.
        /// </summary>
        /// <param name="id">id of the product to update</param>
        /// <param name="request">ProductDTO, contains the necesary data for update to register in the DB</param>
        /// <returns></returns>
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
            var x = new Request<Product> { Data = new Product() { Id = id,Code=request.Data.Code, Name = request.Data.Name, Description = request.Data.Description , Price=request.Data.Price} };
            return Execute<Product, int>(x, _products.UpdateProduct);
        }
        /// <summary>
        /// Updates the stock of a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateStock/{id}")]
        public IActionResult UpdateStockProduct(int id, [FromBody] Request<ProductStockDTO> request)
        {
            if (id == 0)
            {
                return BadRequest(Store.Models.Response.Error("Id invalido"));
            }
            if (request == null)
            {
                return BadRequest(Store.Models.Response.Error("Owner object is null"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(Store.Models.Response.Error("Invalid model object"));
            }
            var x = new Request<Product> { Data = new Product() { Id = id, Stock=request.Data.stock } };
            return Execute<Product, int>(x, _products.UpdateStockProduct);
        }
        /// <summary>
        /// Deletes a product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
                return BadRequest("Id invalido");
            return Execute<int, object>(new Request<int>() { Data = id }, _products.DeleteProduct);
        }
        /// <summary>
        /// Register a product entry
        /// </summary>
        /// <param name="request">ProductEntryDTO </param>
        /// <returns></returns>
        [Route("RegisterEntry")]
        [HttpPost]
        public IActionResult RegisterEntry([FromBody] Request<ProductEntryDTO> request)
        {

            if (request.Data == null)
            {
                return BadRequest("Objeto ProductDTO es nulo");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo de objecto invalido");
            }
            return Execute<ProductEntryDTO, int>(request, _products.RegisterEntry);
        }
    }
}
