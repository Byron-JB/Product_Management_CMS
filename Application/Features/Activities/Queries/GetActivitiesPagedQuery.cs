using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Activities.Queries
{
    public record GetActivitiesPagedQuery(int PageNumber, int PageSize) : IRequest<PagedResult<Activity>>;
}
