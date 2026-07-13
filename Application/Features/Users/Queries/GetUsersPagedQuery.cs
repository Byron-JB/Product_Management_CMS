using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries
{
    public record GetUsersPagedQuery(int PageNumber, int PageSize) : IRequest<PagedResult<User>>;
}
