using UserService.Application.Users.Queries.GetUserProfile;

namespace UserService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        
    }
}
