using Application.Features.ProductSizes.Queries;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductSizes.Handlers
{
    public class GetAllProductSizesHandler : IRequestHandler<GetAllProductSizesQuery, IEnumerable<ProductSize>>
    {
        private readonly IProductSizeRepository _repo;
        public GetAllProductSizesHandler(IProductSizeRepository repo) => _repo = repo;

        public async Task<IEnumerable<ProductSize>> Handle(GetAllProductSizesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }

    public class GetProductSizeByIdHandler : IRequestHandler<GetProductSizeByIdQuery, ProductSize?>
    {
        private readonly IProductSizeRepository _repo;
        public GetProductSizeByIdHandler(IProductSizeRepository repo) => _repo = repo;

        public async Task<ProductSize?> Handle(GetProductSizeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
