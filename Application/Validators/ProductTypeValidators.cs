using Application.Features.ProductTypes.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class CreateProductTypeCommandValidator : AbstractValidator<CreateProductTypeCommand>
    {
        public CreateProductTypeCommandValidator()
        {
            RuleFor(x => x.ProductType).NotNull();
            RuleFor(x => x.ProductType.strType).NotEmpty().MaximumLength(100);
        }
    }

    public class UpdateProductTypeCommandValidator : AbstractValidator<UpdateProductTypeCommand>
    {
        public UpdateProductTypeCommandValidator()
        {
            RuleFor(x => x.ProductType).NotNull();
            RuleFor(x => x.ProductType.iId).GreaterThan(0);
            RuleFor(x => x.ProductType.strType).NotEmpty().MaximumLength(100);
        }
    }
}
