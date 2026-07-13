using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record CreateUserCommand(User User) : IRequest<User>;
}
