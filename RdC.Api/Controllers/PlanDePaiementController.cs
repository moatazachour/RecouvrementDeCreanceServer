﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.PlanDePaiements.Commands.CreatePlan;
using RdC.Application.PlanDePaiements.Commands.LockPlan;
using RdC.Application.PlanDePaiements.Queries.GetPlan;
using RdC.Application.PlanDePaiements.Queries.ListPlans;
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


        [HttpGet]
        [ProducesResponseType(typeof(List<PlanDePaiementResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListPlanDePaiements()
        {
            try
            {
                var query = new ListPlansQuery();

                var listDePlanDePaiement = await _mediator.Send(query);

                return Ok(listDePlanDePaiement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
                throw;
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PlanDePaiementResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlanById([FromRoute] int id)
        {
            var query = new GetPlanQuery(id);

            try
            {
                var plan = await _mediator.Send(query);

                return Ok(plan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [HttpPut("Lock/{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LockPlan([FromRoute] int id)
        {
            var command = new LockPlanCommand(id);

            try
            {
                var isLocked = await _mediator.Send(command);

                if (!isLocked)
                    return NotFound($"Plan with ID {id} not found.");

                return Ok(isLocked);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
