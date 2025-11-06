using UserService.Application.Users.Queries.GetUserProfile;
using UserService.Domain.Entities;
namespace UserService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);

        Task<User?> DeleteUserAsync(int userId);
        
    }
}
