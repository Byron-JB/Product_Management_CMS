using Application.Features.Users.Queries;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers
{
    public class GetUsersPagedHandler : IRequestHandler<GetUsersPagedQuery, PagedResult<User>>
    {
        private readonly IUserRepository _repo;
        public GetUsersPagedHandler(IUserRepository repo) => _repo = repo;

        public Task<PagedResult<User>> Handle(GetUsersPagedQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
