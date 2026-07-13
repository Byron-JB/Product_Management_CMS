using Application.Features.ProductSizes.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class CreateProductSizeCommandValidator : AbstractValidator<CreateProductSizeCommand>
    {
        public CreateProductSizeCommandValidator()
        {
            RuleFor(x => x.ProductSize).NotNull();
            RuleFor(x => x.ProductSize.strSize).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ProductSize.iStockOnHand).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ProductSize.iIncomingStock).GreaterThanOrEqualTo(0);
        }
    }

    public class UpdateProductSizeCommandValidator : AbstractValidator<UpdateProductSizeCommand>
    {
        public UpdateProductSizeCommandValidator()
        {
            RuleFor(x => x.ProductSize).NotNull();
            RuleFor(x => x.ProductSize.iId).GreaterThan(0);
            RuleFor(x => x.ProductSize.strSize).NotEmpty().MaximumLength(50);
        }
    }
}
