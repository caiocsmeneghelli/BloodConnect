using BloodConnect.Application.Queries.GetAllBloodStock;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodStockController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public BloodStockController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> Get() { 
            var result = await _mediatr.Send(new GetAllBloodStockQuery());
            return Ok(result);
        }
    }
}
