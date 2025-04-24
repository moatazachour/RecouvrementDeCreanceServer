using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Paiements.Commands.CreatePaiement;
using RdC.Application.Paiements.Queries.GetPaiement;
using RdC.Domain.DTO.Paiement;

namespace RdC.Api.Controllers.Paiements
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaiementController : ControllerBase
    {
        private readonly ISender _mediator;

        public PaiementController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PaiementResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaiementById([FromRoute] int id)
        {
            var query = new GetPaiementQuery(id);

            try
            {
                var paiement = await _mediator.Send(query);

                return Ok(paiement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPaiement([FromBody] CreatePaiementRequest request)
        {
            var command = new CreatePaiementCommand(request);

            try
            {
                int paiementId = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetPaiementById), new { id = paiementId }, paiementId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
