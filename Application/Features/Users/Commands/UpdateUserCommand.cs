using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record UpdateUserCommand(User User) : IRequest;
}
