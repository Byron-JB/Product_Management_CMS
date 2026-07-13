using Domain.Entities;
using MediatR;

namespace Application.Features.ProductTypes.Queries
{
    public record GetProductTypeByIdQuery(int Id) : IRequest<ProductType?>;
}
