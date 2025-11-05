using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Application.Users.Queries.GetUserProfile;
using UserService.Infrastructure.Data;
using UserService.Application.Users.Commands.UserCommands;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public UserController(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        //get request to retrieve user's info (email, id, username)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(string id)
        {

            try
            {
                var query = new GetUserProfileQuery { UserId = id };
                var result = await _mediator.Send(query);

                if (result == null)
                    return NotFound($"User with ID {id} not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //delete specific user
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var command = new DeleteUserCommand { UserId = id };
                var result = await _mediator.Send(command);

                if (!result)
                    return NotFound($"User with ID {id} not found.");

                return StatusCode(200,new { message = $"User with ID {id} deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            } 
        }
    }
}
