using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{   
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3 - Chapter 6
* Created by: 	Jeff Anderson Charriot - 2133124
* Date: 		7 December 2023
* Class Name: 	ProductsController.cs
* Description: 
The ProductsController is an ASP.NET Core API controller for an e-commerce system, handling HTTP requests to retrieve all products or a specific product by ID.
    It relies on an injected IProductsProvider.
*/
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var all = await productsProvider.GetProductsAsync();
            if (all.IsSuccess)
            {
                return Ok(all.Products);
            }


            return NotFound(all.ErrorMessage);
        }

        [HttpGet("{id}")]
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