using Application.Features.Products.Queries;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Handlers
{
    public class GetProductsPagedHandler : IRequestHandler<GetProductsPagedQuery, PagedResult<Product>>
    {
        private readonly IProductRepository _repo;
        public GetProductsPagedHandler(IProductRepository repo) => _repo = repo;

        public Task<PagedResult<Product>> Handle(GetProductsPagedQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
