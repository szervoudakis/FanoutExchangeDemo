using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Application.Users.Queries.GetUserProfile;
using UserService.Infrastructure.Data;
using UserService.Application.Users.Commands.UserCommands;
using UserService.WebAPI.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        private readonly RabbitMqPublisher _rabbitMqPublisher;

        public UserController(AppDbContext context, IMediator mediator, RabbitMqPublisher rabbitMqPublisher)
        {
            _context = context;
            _mediator = mediator;
            _rabbitMqPublisher = rabbitMqPublisher;
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
                var deletedUser = await _mediator.Send(command);

                if (deletedUser==null)
                    return NotFound($"User with ID {id} not found.");
                _rabbitMqPublisher.PublishUserDeleted(deletedUser.Username, deletedUser.Email);
                return StatusCode(200,new { message = $"User with ID {id} deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            } 
        }
    }
}
