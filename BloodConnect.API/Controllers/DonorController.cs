using BloodConnect.Application.Commands.CreateDonor;
using BloodConnect.Application.Queries.GetAllDonors;
using BloodConnect.Application.Queries.GetDonorById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public DonorController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetAllDonorsQuery query = new GetAllDonorsQuery();
            var results = await _mediatr.Send(query);
            return Ok(results);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            GetDonorByIdQuery query = new GetDonorByIdQuery(id);
            var donor = await _mediatr.Send(query);
            if(donor is null) { return NotFound(); }

            return Ok(donor);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateDonorCommand command)
        {
            var result = await _mediatr.Send(command);
            if(result.IsSuccess == false) { BadRequest(result.Errors); }

            return Ok(result.Value);
        }
    }
}
