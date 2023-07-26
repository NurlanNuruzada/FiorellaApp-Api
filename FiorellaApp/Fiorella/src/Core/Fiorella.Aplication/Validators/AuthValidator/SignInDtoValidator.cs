using Fiorello.Application.DTOs.AuthDTOs;
using FluentValidation;

namespace Fiorello.Application.Validators.AuthValidator
{
    public class SignInDtoValidator : AbstractValidator<SignInDto>
    {
        public SignInDtoValidator()
        {
            RuleFor(dto => dto.UsernameOrEmail).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Username or Email is required.")
                .MaximumLength(255);

            RuleFor(dto => dto.password).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(150)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
