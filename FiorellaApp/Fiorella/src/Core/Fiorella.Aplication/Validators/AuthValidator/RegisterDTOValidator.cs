using Fiorello.Application.DTOs.AuthDTOs;
using FluentValidation;

namespace Fiorello.Application.Validators.AuthValidator
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(dto => dto.fullname).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Fullname is required.")
                .MaximumLength(150)
                .Matches("^[a-zA-Z ]+$").WithMessage("Fullname can only contain letters.");

            RuleFor(dto => dto.username).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(50)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(dto => dto.email).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(255)
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(dto => dto.password).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(150)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
