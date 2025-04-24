using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.PaiementDates.Commands.CreatePaiementDates;
using RdC.Application.PaiementDates.Queries.GetPaiementDate;
using RdC.Domain.DTO.Paiement;
using RdC.Domain.DTO.PaiementDate;

namespace RdC.Api.Controllers.PaiementDates
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaiementDatesController : ControllerBase
    {
        private readonly ISender _mediator;

        public PaiementDatesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePaiementDates([FromBody] CreatePaiementDatesRequest request)
        {
            var command = new CreatePaiementDatesCommand(request);

            try
            {
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PaiementDateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaiementDateByID([FromRoute] int id)
        {
            var query = new GetPaiementDateQuery(id);

            try
            {
                var paiementDate = await _mediator.Send(query);

                return Ok(paiementDate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
