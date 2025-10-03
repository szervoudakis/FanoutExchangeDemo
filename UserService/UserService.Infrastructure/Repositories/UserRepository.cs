using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Application.Users.Queries.GetUserProfile;
using UserService.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserProfileDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email
            })
            .FirstOrDefaultAsync();
    }
}
