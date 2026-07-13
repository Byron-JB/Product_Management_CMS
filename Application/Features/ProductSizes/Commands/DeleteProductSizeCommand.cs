using MediatR;

namespace Application.Features.ProductSizes.Commands
{
    public record DeleteProductSizeCommand(int Id) : IRequest;
}
