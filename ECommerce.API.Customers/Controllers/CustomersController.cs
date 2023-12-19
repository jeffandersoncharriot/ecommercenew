using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3 - Chapter 6
* Created by: 	Jeff Anderson Charriot - 2133124
* Date: 		19 December 2023
* Class Name: 	CustomersController.cs
* Description: 	
* ECommerce.Api.Customers.Controllers namespace is an ASP.NET Core API controller handling HTTP requests for customer-related operations
* in an e-commerce system. It relies on an injected ICustomersProvider to retrieve customer data and provides endpoints for listing all customers 
* and retrieving an individual customer by ID.
   */

    [ApiController]
    [Route("api/customers")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/customers/
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "John Keyboard",
        ///             "address": "123334 rue Maple"
        ///         },
        ///         {
        ///             "id": 2,
        ///             "name": "John Monitor",
        ///             "address": "123 rue Maple"
        ///         },
        ///         {
        ///             "id": 3,
        ///             "name": "Marcov CPU",
        ///             "address": "12 rue Maple"
        ///         }
        ///     ]
        /// </remarks>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns all customers</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Customer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();

            if (result.IsSuccess)
                return Ok(result.Customers);
            
            return NotFound();
        }

        /// <summary>
        /// Gets customer by provided id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/customers/1
        ///     {
        ///         "id": 1,
        ///         "name": "John Keyboard",
        ///         "address": "123334 rue Maple"
        ///     }
        /// </remarks>
        /// <param name="id">The customer id to get</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested customer</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);



            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}