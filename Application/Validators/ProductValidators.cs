using Application.Features.Products.Commands;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product).NotNull();
            RuleFor(x => x.Product.strName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Product.dblprice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Product.ProductTypeId).GreaterThan(0);
        }
    }

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Product).NotNull();
            RuleFor(x => x.Product.iId).GreaterThan(0);
            RuleFor(x => x.Product.strName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Product.dblprice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Product.ProductTypeId).GreaterThan(0);
        }
    }
}
