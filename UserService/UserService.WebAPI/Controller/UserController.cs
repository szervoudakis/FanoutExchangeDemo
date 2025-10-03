using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;
using UserService.Application.Users.Queries.GetUserProfile;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IMediator mediator;

        public UserController(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            try
            {
                // Read username from JWT claims (read JWT bearer token and take username)
                var userid = UserFindFirst(ClaimTypes.NameIdentified)?.Value;

                if (string.IsNullOrEmpty(userid))
                    return Unauthorized("Invalid token or user info missing");

                var query = new GetUserProfileQuery { UserId = userId };
                var result = await _mediator.Send(query);

                if (result == null)
                    return NotFound();

                return Ok(result);
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
