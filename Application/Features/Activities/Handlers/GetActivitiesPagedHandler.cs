using Application.Features.Activities.Queries;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Activities.Handlers
{
    public class GetActivitiesPagedHandler : IRequestHandler<GetActivitiesPagedQuery, PagedResult<Activity>>
    {
        private readonly IActivityRepository _repo;
        public GetActivitiesPagedHandler(IActivityRepository repo) => _repo = repo;

        public Task<PagedResult<Activity>> Handle(GetActivitiesPagedQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
