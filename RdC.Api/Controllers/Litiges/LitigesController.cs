using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Litiges.Commands.CreateLitige;
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

        [HttpPost]
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


        [HttpPost("litiges/{id:int}/justificatifs")]
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


        [HttpGet("{id:int}")]
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
    }
}
