using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.LitigeTypes.Commands.CreateLitigeType;
using RdC.Application.LitigeTypes.Commands.UpdateLitigeType;
using RdC.Application.LitigeTypes.Queries.GetAllLitigeTypes;
using RdC.Application.LitigeTypes.Queries.GetLitigeType;
using RdC.Domain.DTO.LitigeType;

namespace RdC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LitigeTypesController : ControllerBase
    {
        private readonly ISender _mediator;

        public LitigeTypesController(ISender sender)
        {
            _mediator = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLitigeType([FromBody] LitigeTypeRequest request)
        {
            var command = new CreateLitigeTypeCommand(request.LitigeTypeName, request.LitigeTypeDescription);

            try
            {
                int litigeTypeID = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetLitigeTypeById), new { id = litigeTypeID }, litigeTypeID);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLitigeType([FromRoute] int id, [FromBody] LitigeTypeRequest request)
        {
            var command = new UpdateLitigeTypeCommand(id, request.LitigeTypeName, request.LitigeTypeDescription);

            try
            {
                var updatedLitigeType = await _mediator.Send(command);

                if (updatedLitigeType is null)
                {
                    return NotFound($"Litige type with ID {id} is not found!");
                }

                return Ok(updatedLitigeType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(LitigeTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLitigeTypeById([FromRoute] int id)
        {
            var query = new GetLitigeTypeQuery(id);

            try
            {
                var litigeType = await _mediator.Send(query);

                return Ok(litigeType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LitigeTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLitigeTypes()
        {
            var query = new GetAllLitigeTypesQuery();

            try
            {
                var litigeTypes = await _mediator.Send(query);

                return Ok(litigeTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
