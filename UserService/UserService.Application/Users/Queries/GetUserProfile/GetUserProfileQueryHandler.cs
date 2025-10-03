using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Users.Queries.GetUserProfile;
using UserService.Application.Interfaces;


namespace UserService.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto?>
    {
        private readonly IUserRepository _userRepository;

       public GetUserProfileQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileDto?> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(request.UserId);
            return await _userRepository.GetUserProfileAsync(userId);
        }
    }
}
