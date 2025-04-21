using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Litiges.Commands.CreateLitige;
using RdC.Application.Litiges.Commands.RejectLitige;
using RdC.Application.Litiges.Commands.ResolveAmountError;
using RdC.Application.Litiges.Commands.ResolveDuplicated;
using RdC.Application.Litiges.Commands.UploadLitigeJustificatifs;
using RdC.Application.Litiges.Queries.GetLitige;
using RdC.Application.Litiges.Queries.GetLitiges;
using RdC.Domain.DTO.Litige;

namespace RdC.Api.Controllers.Litiges
{
    [Route("api/[controller]")]
    [ApiController]
    public class LitigesController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ILogger<LitigesController> _logger;

        public LitigesController(
            ISender mediator, 
            ILogger<LitigesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(LitigeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLitigeById([FromRoute] int id)
        {
            var query = new GetLitigeQuery(id);

            try
            {
                var litige = await _mediator.Send(query);

                return Ok(litige);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(List<LitigeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLitiges()
        {
            var query = new GetLitigesQuery();

            try
            {
                var litiges = await _mediator.Send(query);

                return Ok(litiges);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload justificatifs.");
                return StatusCode(500, "Something went wrong while uploading justificatifs.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLitige(
            [FromBody] CreateLitigeRequest request)
        {
            var command = new CreateLitigeCommand(request);

            try
            {
                int litigeID = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetLitigeById), new { id = litigeID }, litigeID);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("{id:int}/justificatifs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadJustificatifs(
            [FromRoute] int id,
            [FromForm] List<IFormFile> files)
        {
            var fileDtos = files.Select(file => new FileDto
            {
                FileName = file.FileName,
                Content = file.OpenReadStream()
            }).ToList();

            var command = new UploadLitigeJustificatifsCommand(
                LitigeID: id,
                Files: fileDtos);

            try
            {
                await _mediator.Send(command);

                return Ok(new { message = "Justificatifs uploaded" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload justificatifs.");
                return StatusCode(500, "Something went wrong while uploading justificatifs.");
            }
        }


        [HttpPut("RejectLitige/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RejectLitige([FromRoute] int id)
        { 
            var command = new RejectLitigeCommand( LitigeID: id);

            try
            {
                bool isRejected = await _mediator.Send(command);

                if (isRejected)
                    return Ok($"Litige with ID {id} is rejected");
                

                return NotFound($"Litige with ID {id} not found!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reject litige.");
                return StatusCode(500, "Something went wrong while rejecting litige.");
            }
        }

        [HttpPut("CorrectAmount/{id:int}")]
        public async Task<IActionResult> ResolveAmountError(
            [FromRoute] int id,
            [FromBody] CorrectAmountRequest request)
        {
            var command = new ResolveAmountErrorCommand(
                LitigeID: id,
                CorrectedTotalAmount: request.correctedMontantTotal,
                CorrectedAmountDue: request.correctedAmountDue);

            try
            {
                bool isResolved = await _mediator.Send(command);

                if (isResolved)
                    return Ok($"Litige with ID {id} is resolved");

                return NotFound($"Litige with ID {id} not found!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to resolve amount error.");
                return StatusCode(500, "Something went wrong while resolving litige.");
            }
        }

        [HttpPut("ResolveDuplicated/{id:int}")]
        public async Task<IActionResult> ResolveDuplicated([FromRoute] int id)
        {
            var commond = new ResolveDuplicatedCommand(LitigeID: id);

            try
            {
                bool isResolved = await _mediator.Send(commond);

                if (isResolved)
                    return Ok($"Litige with ID {id} is resolved");

                return Ok($"Litige with ID {id} is rejected");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to resolve duplication error.");
                return StatusCode(500, "Something went wrong while resolving litige.");
            }
        }
    }
}
