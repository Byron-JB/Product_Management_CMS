using MediatR;

namespace Application.Features.Products.Commands
{
    public record DeleteProductCommand(int Id) : IRequest;
}
