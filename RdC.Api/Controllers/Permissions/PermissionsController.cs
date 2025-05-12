using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Permissions.Queries.ListPermissions;
using RdC.Domain.DTO.Role;

namespace RdC.Api.Controllers.Permissions
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly ISender _mediator;

        public PermissionsController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<PermissionDefinitionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPermissions()
        {
            try
            {
                var permissions = await _mediator.Send(new ListPermissionsQuery());

                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
