using MediatR;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands.UserCommands
{
    public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, User?>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(request.UserId);

            return await _userRepository.DeleteUserAsync(userId);
        }
    }
}