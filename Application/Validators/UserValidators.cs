using Application.Features.Users.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.User).NotNull();
            RuleFor(x => x.User.strEmail).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.User.strPasswordHash).NotEmpty();
        }
    }

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.User).NotNull();
            RuleFor(x => x.User.iId).GreaterThan(0);
            RuleFor(x => x.User.strEmail).NotEmpty().EmailAddress().MaximumLength(256);
        }
    }
}
