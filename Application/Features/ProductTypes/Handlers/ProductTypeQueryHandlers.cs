using Application.Features.ProductTypes.Queries;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductTypes.Handlers
{
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IEnumerable<ProductType>>
    {
        private readonly IProductTypeRepository _repo;
        public GetAllProductTypesHandler(IProductTypeRepository repo) => _repo = repo;

        public async Task<IEnumerable<ProductType>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }

    public class GetProductTypeByIdHandler : IRequestHandler<GetProductTypeByIdQuery, ProductType?>
    {
        private readonly IProductTypeRepository _repo;
        public GetProductTypeByIdHandler(IProductTypeRepository repo) => _repo = repo;

        public async Task<ProductType?> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
