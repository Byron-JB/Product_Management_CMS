using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Products.Queries
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<Product>>;
}
