using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Roles.Commands.CreateRole;
using RdC.Application.Roles.Commands.UpdateRole;
using RdC.Application.Roles.Queries.GetRole;
using RdC.Application.Roles.Queries.GetRoles;
using RdC.Domain.DTO.Role;

namespace RdC.Api.Controllers.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ISender _mediator;

        public RolesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRole([FromBody] RoleRequest request)
        {
            var command = new CreateRoleCommand(request);

            try
            {
                int roleId = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetRoleByID), new { id = roleId }, roleId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(RoleResponseWithUsers) ,StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoleByID([FromRoute] int id)
        {
            var query = new GetRoleQuery(id);

            try
            {
                var role = await _mediator.Send(query);

                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("All")]
        [ProducesResponseType(typeof(List<RoleResponseWithUsers>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoles()
        {
            var query = new GetRolesQuery();

            try
            {
                var rolesList = await _mediator.Send(query);

                return Ok(rolesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRole(
            [FromRoute] int id,
            [FromBody] RoleRequest request)
        {
            var command = new UpdateRoleCommand(id, request);

            try
            {
                bool isUpdated = await _mediator.Send(command);

                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
