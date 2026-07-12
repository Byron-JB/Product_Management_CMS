using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record UpdateProductCommand(Product Product) : IRequest;
}
