using Domain.Entities;
using MediatR;

namespace Application.Features.ProductSizes.Commands
{
    public record UpdateProductSizeCommand(ProductSize ProductSize) : IRequest;
}
