using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<User?>;
}
