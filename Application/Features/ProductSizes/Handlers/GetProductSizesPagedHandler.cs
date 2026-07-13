using Application.Features.ProductSizes.Queries;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductSizes.Handlers
{
    public class GetProductSizesPagedHandler : IRequestHandler<GetProductSizesPagedQuery, PagedResult<ProductSize>>
    {
        private readonly IProductSizeRepository _repo;
        public GetProductSizesPagedHandler(IProductSizeRepository repo) => _repo = repo;

        public Task<PagedResult<ProductSize>> Handle(GetProductSizesPagedQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
