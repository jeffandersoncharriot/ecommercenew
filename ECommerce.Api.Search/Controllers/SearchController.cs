using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3 - Chapter 6
* Created by: 	Jeff Anderson Charriot - 2133124
* Date: 		7 December 2023
* Class Name: 	SearchController.cs
* Description: 
The SearchController is an ASP.NET Core API controller, marked with [ApiController] and [Route("api/search")], dedicated to handling HTTP POST requests for searching in an e-commerce system. It relies on an injected ISearchService and returns search results if successful, 
    otherwise a 404 Not Found response.
*/
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
        
        [HttpPost]
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