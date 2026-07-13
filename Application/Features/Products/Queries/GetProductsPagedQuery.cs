using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetProductsPagedQuery(int PageNumber, int PageSize) : IRequest<PagedResult<Product>>;
}
