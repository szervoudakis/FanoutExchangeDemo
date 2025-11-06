using MediatR;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands.UserCommands
{
    public class DeleteUserCommand: IRequest<User?>
    {
        public string UserId { get; set; } = string.Empty;
    }
}