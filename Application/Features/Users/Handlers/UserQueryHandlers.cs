using Application.Features.Users.Queries;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _repo;
        public GetAllUsersHandler(IUserRepository repo) => _repo = repo;

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User?>
    {
        private readonly IUserRepository _repo;
        public GetUserByIdHandler(IUserRepository repo) => _repo = repo;

        public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
