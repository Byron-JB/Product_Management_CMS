using Application.Features.Users.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _repo;
        public CreateUserHandler(IUserRepository repo) => _repo = repo;

        public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return _repo.CreateAsync(request.User, cancellationToken);
        }
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _repo;
        public UpdateUserHandler(IUserRepository repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateAsync(request.User, cancellationToken);
            return Unit.Value;
        }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repo;
        public DeleteUserHandler(IUserRepository repo) => _repo = repo;

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
