using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Acheteurs.Commands.AddAcheteurs;
using RdC.Application.Acheteurs.Queries.GetAcheteur;
using RdC.Application.Acheteurs.Queries.ListAcheteurs;
using RdC.Domain.Acheteurs;
using RdC.Domain.DTO.Acheteur;
using RdC.Domain.DTO.Facture;

namespace RdC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcheteursController : ControllerBase
    {
        private readonly ISender _mediator;

        public AcheteursController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<AcheteurResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAcheteurs()
        {
            try
            {
                var query = new ListAcheteursQuery();

                var listAcheteursResult = await _mediator.Send(query);

                var listAcheteurs = listAcheteursResult.Select(acheteur =>
                            new AcheteurResponse(
                                acheteur.AcheteurID,
                                acheteur.Nom,
                                acheteur.Prenom,
                                acheteur.Adresse,
                                acheteur.Email,
                                acheteur.Telephone,
                                acheteur.Factures.Select(facture => new FactureResponse(
                                                            facture.FactureID,
                                                            facture.NumFacture,
                                                            facture.DateEcheance,
                                                            facture.MontantTotal,
                                                            facture.MontantRestantDue,
                                                            facture.AcheteurID,
                                                            facture.Status)).ToList()))
                            .ToList();

                return Ok(listAcheteurs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshAcheteurs()
        {
            try
            {
                var command = new AddAcheteursCommand();

                bool isRefreshed = await _mediator.Send(command);

                if (isRefreshed) 
                    return Ok("Refreshed");

                return BadRequest("System de Facturation Not Running");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AcheteurResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAcheteur([FromRoute] int id)
        {
            try
            {
                var query = new GetAcheteurQuery(id);

                var acheteur = await _mediator.Send(query);

                if (acheteur is null)
                {
                    return NotFound($"Acheteur with ID {id} not found.");
                }

                return Ok(new AcheteurResponse(
                    acheteur.AcheteurID,
                    acheteur.Nom,
                    acheteur.Prenom,
                    acheteur.Adresse,
                    acheteur.Email,
                    acheteur.Telephone,
                    acheteur.Factures.Select(facture => new FactureResponse(
                                                            facture.FactureID,
                                                            facture.NumFacture,
                                                            facture.DateEcheance,
                                                            facture.MontantTotal,
                                                            facture.MontantRestantDue,
                                                            facture.AcheteurID,
                                                            facture.Status)).ToList()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
