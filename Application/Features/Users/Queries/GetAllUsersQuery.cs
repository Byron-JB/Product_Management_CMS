using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Users.Queries
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<User>>;
}
