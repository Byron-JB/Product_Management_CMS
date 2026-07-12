using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record CreateProductCommand(Product Product) : IRequest<Product>;
}
