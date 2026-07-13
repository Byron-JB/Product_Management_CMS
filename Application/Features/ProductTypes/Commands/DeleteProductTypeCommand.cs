using MediatR;

namespace Application.Features.ProductTypes.Commands
{
    public record DeleteProductTypeCommand(int Id) : IRequest;
}
