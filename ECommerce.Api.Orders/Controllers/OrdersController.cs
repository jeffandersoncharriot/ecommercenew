using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Jeff Anderson Charriot - 2133124
* Date: 		23 November 2023
* Class Name: 	OrdersController.cs
* Description: 	

The OrdersController class, marked with [ApiController] and [Route("api/orders")], is an ASP.NET Core API controller handling HTTP requests for retrieving orders associated with a specific customer. 
    It depends on an injected IOrdersProvider and provides
    a single endpoint (GetOrdersAsync) that returns orders for a given customer ID if successful, or a 404 Not Found response otherwise.
*/
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
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