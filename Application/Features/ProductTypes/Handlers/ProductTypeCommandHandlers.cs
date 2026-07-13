using Application.Features.ProductTypes.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductTypes.Handlers
{
    public class CreateProductTypeHandler : IRequestHandler<CreateProductTypeCommand, ProductType>
    {
        private readonly IProductTypeRepository _repo;
        public CreateProductTypeHandler(IProductTypeRepository repo) => _repo = repo;

        public Task<ProductType> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            return _repo.CreateAsync(request.ProductType, cancellationToken);
        }
    }

    public class UpdateProductTypeHandler : IRequestHandler<UpdateProductTypeCommand>
    {
        private readonly IProductTypeRepository _repo;
        public UpdateProductTypeHandler(IProductTypeRepository repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateAsync(request.ProductType, cancellationToken);
            return Unit.Value;
        }
    }

    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand>
    {
        private readonly IProductTypeRepository _repo;
        public DeleteProductTypeHandler(IProductTypeRepository repo) => _repo = repo;

        public async Task<Unit> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
