using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{   
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3 - Chapter 6
* Created by: 	Jeff Anderson Charriot - 2133124
* Date: 		19 December 2023
* Class Name: 	ProductsController.cs
* Description: 
The ProductsController is an ASP.NET Core API controller for an e-commerce system, handling HTTP requests to retrieve all products or a specific product by ID.
    It relies on an injected IProductsProvider.
*/
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/products/
        ///     
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "Keyboard",
        ///             "price": 20,
        ///             "inventory": 100
        ///         },
        ///         {
        ///             "id": 2,
        ///             "name": "Mouse",
        ///             "price": 5,
        ///             "inventory": 200
        ///         },
        ///         {
        ///             "id": 3,
        ///             "name": "Monitor",
        ///             "price": 150,
        ///             "inventory": 1000
        ///         },
        ///         {
        ///             "id": 4,
        ///             "name": "CPU",
        ///             "price": 200,
        ///             "inventory": 2000
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the list of products</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductsAsync()
        {
            var all = await productsProvider.GetProductsAsync();
            if (all.IsSuccess)
            {
                return Ok(all.Products);
            }


            return NotFound(all.ErrorMessage);
        }

        /// <summary>
        /// Get product by the provided id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/products/1
        ///     {
        ///         "id": 1,
        ///         "name": "Keyboard",
        ///         "price": 20,
        ///         "inventory": 100
        ///     }
        /// </remarks>
        /// <param name="id">The product id to get</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested product</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsProvider.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound(result.ErrorMessage);
        }
    }
}