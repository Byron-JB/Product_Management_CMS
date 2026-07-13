using Application.Features.ProductSizes.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductSizes.Handlers
{
    public class CreateProductSizeHandler : IRequestHandler<CreateProductSizeCommand, ProductSize>
    {
        private readonly IProductSizeRepository _repo;
        public CreateProductSizeHandler(IProductSizeRepository repo) => _repo = repo;

        public Task<ProductSize> Handle(CreateProductSizeCommand request, CancellationToken cancellationToken)
        {
            return _repo.CreateAsync(request.ProductSize, cancellationToken);
        }
    }

    public class UpdateProductSizeHandler : IRequestHandler<UpdateProductSizeCommand>
    {
        private readonly IProductSizeRepository _repo;
        public UpdateProductSizeHandler(IProductSizeRepository repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateProductSizeCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateAsync(request.ProductSize, cancellationToken);
            return Unit.Value;
        }
    }

    public class DeleteProductSizeHandler : IRequestHandler<DeleteProductSizeCommand>
    {
        private readonly IProductSizeRepository _repo;
        public DeleteProductSizeHandler(IProductSizeRepository repo) => _repo = repo;

        public async Task<Unit> Handle(DeleteProductSizeCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
