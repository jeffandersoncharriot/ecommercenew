using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3 - Chapter 6
* Created by: 	Jeff Anderson Charriot - 2133124
* Date: 		19 December 2023
* Class Name: 	OrdersController.cs
* Description: 	

The OrdersController class, marked with [ApiController] and [Route("api/orders")], is an ASP.NET Core API controller handling HTTP requests for retrieving orders associated with a specific customer. 
    It depends on an injected IOrdersProvider and provides
    a single endpoint (GetOrdersAsync) that returns orders for a given customer ID if successful, or a 404 Not Found response otherwise.
*/
    [ApiController]
    [Route("api/orders")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        /// <summary>
        /// Get orders by the provided customer id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/orders/1
        ///     [
        ///         {
        ///             "id": 1,
        ///             "customerId": 1,
        ///             "orderDate": "2023-12-17T20:36:38.4351941-05:00",
        ///             "total": 100,
        ///             "items": [
        ///                 {
        ///                     "id": 1,
        ///                     "productId": 1,
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 2,
        ///                     "productId": 2,
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 3,
        ///                     "productId": 3,
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 4,
        ///                     "productId": 2,
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 5,
        ///                     "productId": 3,
        ///                     "quantity": 1,
        ///                     "unitPrice": 100
        ///                 }
        ///             ]
        ///         },
        ///         {
        ///             "id": 2,
        ///             "customerId": 1,
        ///             "orderDate": "2023-12-16T20:36:38.5018155-05:00",
        ///             "total": 100,
        ///             "items": [
        ///                 {
        ///                     "id": 6,
        ///                     "productId": 1,
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 7,
        ///                     "productId": 2,
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 8,
        ///                     "productId": 3,
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 9,
        ///                     "productId": 2,
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 10,
        ///                     "productId": 3,
        ///                     "quantity": 1,
        ///                     "unitPrice": 100
        ///                 }
        ///             ]
        ///         }
        ///     ]
        /// </remarks>
        /// <param name="customerId">The customer id to get their orders</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested orders from the customer</response>
        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}