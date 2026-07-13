using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProductSizes.Queries
{
    public record GetProductSizesPagedQuery(int PageNumber, int PageSize) : IRequest<PagedResult<ProductSize>>;
}
