using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Acheteurs.Queries.GetAcheteur;
using RdC.Application.Acheteurs.Queries.ListAcheteurs;
using RdC.Contracts.Acheteurs;

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

        [HttpGet]
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
                                acheteur.Telephone))
                            .ToList();

                return Ok(listAcheteurs);
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

                var getAcheteurResult = await _mediator.Send(query);

                if (getAcheteurResult is null)
                {
                    return NotFound($"Acheteur with ID {id} not found.");
                }

                return Ok(new AcheteurResponse(
                    getAcheteurResult.AcheteurID,
                    getAcheteurResult.Nom,
                    getAcheteurResult.Prenom,
                    getAcheteurResult.Adresse,
                    getAcheteurResult.Email,
                    getAcheteurResult.Telephone));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
