using Application.Features.ProductTypes.Queries;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductTypes.Handlers
{
    public class GetProductTypesPagedHandler : IRequestHandler<GetProductTypesPagedQuery, PagedResult<ProductType>>
    {
        private readonly IProductTypeRepository _repo;
        public GetProductTypesPagedHandler(IProductTypeRepository repo) => _repo = repo;

        public Task<PagedResult<ProductType>> Handle(GetProductTypesPagedQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
