using Domain.Entities;
using MediatR;

namespace Application.Features.ProductTypes.Commands
{
    public record CreateProductTypeCommand(ProductType ProductType) : IRequest<ProductType>;
}
