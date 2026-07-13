using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.ProductSizes.Queries
{
    public record GetAllProductSizesQuery() : IRequest<IEnumerable<ProductSize>>;
}
