using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProductTypes.Queries
{
    public record GetProductTypesPagedQuery(int PageNumber, int PageSize) : IRequest<PagedResult<ProductType>>;
}
