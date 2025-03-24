using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.PlanDePaiements.Commands;
using RdC.Application.PlanDePaiements.Commands.CreatePlan;
using RdC.Application.PlanDePaiements.Queries.GetPlan;
using RdC.Domain.DTO.PlanDePaiement;

namespace RdC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanDePaiementController : ControllerBase
    {
        private readonly ISender _mediator;

        public PlanDePaiementController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPlanById([FromRoute] int id)
        {
            var query = new GetPlanQuery(id);

            try
            {
                var listDePlanDePaiement = await _mediator.Send(query);

                return Ok(listDePlanDePaiement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddPlanDePaiement([FromBody] CreatePlanDePaiementRequest request)
        {
            if (request is null)
                return BadRequest("Request cannot be null.");

            var command = new CreatePlanCommand(request);

            try
            {
                var planId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetPlanById), new { id = planId }, planId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
