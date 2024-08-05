using BloodConnect.Application.Commands.CreateDonation;
using BloodConnect.Application.Queries.GetAllDonation;
using BloodConnect.Application.Queries.GetAllDonationsByDonor;
using BloodConnect.Application.Queries.GetDonationById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mediator.Send(new GetAllDonationQuery());
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDonationByIdQuery(id);
            var donation = await _mediator.Send(query);
            if (donation == null)
            {
                return NotFound();
            }

            return Ok(donation);
        }

        [HttpGet("donor/{id}")]
        public async Task<IActionResult> GetAllDonationsByDonor(int id)
        {
            var query = new GetAllDonationsByDonorQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDonationCommand command)
        {
            var idDonation = await _mediator.Send(command);
            if (idDonation == 0)
            {
                return BadRequest("Falha ao criar doação.");
            }

            return CreatedAtAction(nameof(GetById), new { id = idDonation }, command);
        }
    }
}
