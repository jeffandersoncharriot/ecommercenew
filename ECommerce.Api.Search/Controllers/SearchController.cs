using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3 - Chapter 6
* Created by: 	Jeff Anderson Charriot - 2133124
* Date: 		19 December 2023
* Class Name: 	SearchController.cs
* Description: 
The SearchController is an ASP.NET Core API controller, marked with [ApiController] and [Route("api/search")], dedicated to handling HTTP POST requests for searching in an e-commerce system. It relies on an injected ISearchService and returns search results if successful, 
    otherwise a 404 Not Found response.
*/
    [ApiController]
    [Route("api/search")]
    [Produces("application/json")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }






        /// <summary>
        /// Searches for orders from the specified customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/search 
        ///     {
        ///         "customerId": 2
        ///     }
        /// 
        ///     {
        ///         "customer": {
        ///             "id": 2,
        ///             "name": "John Monitor",
        ///             "address": "123 rue Maple"
        ///         },
        ///         "orders": [
        ///             {
        ///                 "id": 3,
        ///                 "orderDate": "2023-12-18T11:07:20.8026003-05:00",
        ///                 "total": 100,
        ///                 "items": [
        ///                     {
        ///                         "id": 11,
        ///                         "productId": 1,
        ///                         "productName": "Keyboard",
        ///                         "quantity": 10,
        ///                         "unitPrice": 10
        ///                     },
        ///                     {
        ///                         "id": 12,
        ///                         "productId": 2,
        ///                         "productName": "Mouse",
        ///                         "quantity": 10,
        ///                         "unitPrice": 10
        ///                     },
        ///                     {
        ///                         "id": 13,
        ///                         "productId": 3,
        ///                         "productName": "Monitor",
        ///                         "quantity": 1,
        ///                         "unitPrice": 100
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="termSearch">The search term object containing the customerId.</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested customer with their orders</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchAsync(SearchTerm termSearch)
        {
            var result = await searchService.SearchAsync(termSearch.CustomerId);



            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }


            return NotFound();
        }
    }
}