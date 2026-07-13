using Domain.Entities;
using MediatR;

namespace Application.Features.ProductSizes.Queries
{
    public record GetProductSizeByIdQuery(int Id) : IRequest<ProductSize?>;
}
