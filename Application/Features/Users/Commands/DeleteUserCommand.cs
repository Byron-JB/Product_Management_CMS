using MediatR;

namespace Application.Features.Users.Commands
{
    public record DeleteUserCommand(int Id) : IRequest;
}
