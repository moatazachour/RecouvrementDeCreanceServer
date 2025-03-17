using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Factures.Commands.UpdateFacture;
using RdC.Application.Factures.Queries.GetFacture;
using RdC.Application.Factures.Queries.ListFactures;
using RdC.Domain.DTO.Facture;
using RdC.Domain.Factures;
using System.Text.RegularExpressions;

namespace RdC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturesController : ControllerBase
    {
        private readonly ISender _mediator;

        public FacturesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<FactureResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListFactures()
        {
            try
            {
                var query = new ListFacturesQuery();

                var listFacturesResult = await _mediator.Send(query);

                var listFactures = listFacturesResult.Select(facture =>
                            new FactureResponse(
                                facture.FactureID,
                                facture.NumFacture,
                                facture.DateEcheance,
                                facture.MontantTotal,
                                facture.MontantRestantDue,
                                facture.AcheteurID,
                                Enum.Parse<FactureStatus>(facture.Status.ToString())))
                            .ToList();

                return Ok(listFactures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FactureResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFacture([FromRoute] int id)
        {
            try
            {
                var query = new GetFactureQuery(id);

                var facture = await _mediator.Send(query);

                if (facture is null)
                {
                    return NotFound($"Facture with ID {id} not found.");
                }

                return Ok(new FactureResponse(
                    facture.FactureID,
                    facture.NumFacture,
                    facture.DateEcheance,
                    facture.MontantTotal,
                    facture.MontantRestantDue,
                    facture.AcheteurID,
                    Enum.Parse<FactureStatus>(facture.Status.ToString())));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateFacture([FromRoute] int id, [FromBody] FactureUpdate factureUpdate)
        {
            try
            {
                var command = new UpdateFactureCommand(id, factureUpdate);
                var updatedFacture = await _mediator.Send(command);

                if (updatedFacture is null)
                {
                    return NotFound($"Facture with ID {id} not found.");
                }
                
                return Ok(new FactureResponse(
                    updatedFacture.FactureID,
                    updatedFacture.NumFacture,
                    updatedFacture.DateEcheance,
                    updatedFacture.MontantTotal,
                    updatedFacture.MontantRestantDue,
                    updatedFacture.AcheteurID,
                    Enum.Parse<FactureStatus>(updatedFacture.Status.ToString())));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
