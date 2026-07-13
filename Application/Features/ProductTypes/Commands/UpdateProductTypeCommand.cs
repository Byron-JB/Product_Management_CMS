using Domain.Entities;
using MediatR;

namespace Application.Features.ProductTypes.Commands
{
    public record UpdateProductTypeCommand(ProductType ProductType) : IRequest;
}
