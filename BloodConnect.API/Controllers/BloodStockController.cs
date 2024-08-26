using BloodConnect.Application.Commands.WithdrawBlood;
using BloodConnect.Application.Queries.GetAllBloodStock;
using BloodConnect.Application.Queries.GetBloodStockPerBloodTypeRhFactor;
using BloodConnect.Domain.Enums;
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
        public async Task<IActionResult> Get()
        {
            var result = await _mediatr.Send(new GetAllBloodStockQuery());
            return Ok(result);
        }


        [HttpGet("{bloodType}/{rhFactor}")]
        public async Task<IActionResult> GetPerBloodTypeRhFactor(BloodType bloodType, RhFactor rhFactor)
        {
            if ((Enum.IsDefined(typeof(BloodType), bloodType)) || (Enum.IsDefined(typeof(RhFactor), rhFactor)))
            { return BadRequest("Dados informados inválidos."); }

            var query = new GetBloodStockPerBloodTypeRhFactorQuery(bloodType, rhFactor);
            var result = await _mediatr.Send(query);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("withdraw/{idBloodStock}")]
        public async Task<IActionResult> Withdraw(WithdrawBloodCommand command)
        {
            var result = await _mediatr.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }
    }
}
