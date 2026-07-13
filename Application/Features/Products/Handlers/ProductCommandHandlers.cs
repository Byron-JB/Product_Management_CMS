using Application.Features.Products.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _repo;
        public CreateProductHandler(IProductRepository repo) => _repo = repo;

        public Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return _repo.CreateAsync(request.Product, cancellationToken);
        }
    }

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repo;
        public UpdateProductHandler(IProductRepository repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateAsync(request.Product, cancellationToken);
            return Unit.Value;
        }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repo;
        public DeleteProductHandler(IProductRepository repo) => _repo = repo;

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
