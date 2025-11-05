using MediatR;

namespace UserService.Application.Users.Commands.UserCommands
{
    public class DeleteUserCommand: IRequest<bool>
    {
        public string UserId { get; set; } = string.Empty;
    }
}