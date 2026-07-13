using Domain.Entities;
using MediatR;

namespace Application.Features.ProductSizes.Commands
{
    public record CreateProductSizeCommand(ProductSize ProductSize) : IRequest<ProductSize>;
}
