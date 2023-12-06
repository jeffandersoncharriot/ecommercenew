using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Jeff Anderson Charriot - 2133124
* Date: 		23 November 2023
* Class Name: 	CustomersController.cs
* Description: 	
* ECommerce.Api.Customers.Controllers namespace is an ASP.NET Core API controller handling HTTP requests for customer-related operations
* in an e-commerce system. It relies on an injected ICustomersProvider to retrieve customer data and provides endpoints for listing all customers 
* and retrieving an individual customer by ID.
   */

    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();

            if (result.IsSuccess)
                return Ok(result.Customers);
            
            return NotFound();
        }

        [HttpGet("{id}")]
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