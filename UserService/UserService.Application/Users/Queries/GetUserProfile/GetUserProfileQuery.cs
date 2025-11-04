using MediatR;
using UserService.Application.Users.Queries.GetUserProfile;

namespace UserService.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}


 