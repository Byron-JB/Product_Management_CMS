using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.ProductTypes.Queries
{
    public record GetAllProductTypesQuery() : IRequest<IEnumerable<ProductType>>;
}
