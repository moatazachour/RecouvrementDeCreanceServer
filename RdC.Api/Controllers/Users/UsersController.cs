using MediatR;
using Microsoft.AspNetCore.Mvc;
using RdC.Application.Users.Commands.CompleteUserRegistration;
using RdC.Application.Users.Commands.CreateUser;
using RdC.Application.Users.Commands.Login;
using RdC.Application.Users.Queries.GetUser;
using RdC.Application.Users.Queries.GetUsers;
using RdC.Domain.DTO.User;

namespace RdC.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _mediator;

        public UsersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser(
            [FromBody] AddUserRequest request)
        {
            var command = new CreateUserCommand(
                request.email,
                request.roleID);

            try
            {
                int userID = await _mediator.Send(command);

                if (userID == -1)
                {
                    return Conflict("Email already exist!");
                }

                return Ok(userID);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByID([FromRoute] int id)
        {
            var query = new GetUserQuery(id);

            try
            {
                var userResponse = await _mediator.Send(query);

                if (userResponse is not null)
                {
                    return Ok(userResponse);
                }

                return NotFound($"User with ID {id} is not found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("All")]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetUsersQuery();

            try
            {
                var userList = await _mediator.Send(query);

                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("CompleteRegistration")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompleteRegistration(
            [FromBody] CompleteRegistrationRequest request)
        {
            var command = new CompleteUserRegistrationCommand(
                request.userEmail,
                request.username,
                request.password);

            try
            {
                bool isRegistred = await _mediator.Send(command);

                return Ok(isRegistred);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(
            [FromBody] UserLoginRequest request)
        {
            var command = new LoginCommand(
                request.Identifier,
                request.Password);

            try
            {
                var result = await _mediator.Send(command);

                if (!result.Success)
                {
                    if (result.Message is "Incorrect password." or "Wrong Identifier.")
                    {
                        return Unauthorized(new { error = result.Message });
                    }

                    if (result.Message.Contains("Registration not completed") ||
                        result.Message.Contains("inactive"))
                    {
                        return StatusCode(403, new { error = result.Message });
                    }

                    return BadRequest(new { error = result.Message });
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
