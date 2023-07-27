using Fiorella.Aplication.DTOs.AuthDTOs;
using FluentValidation;

namespace Fiorella.Aplication.Validators.AuthValudator;
public class RegisterDtoValudator:AbstractValidator<RegisterDto>
{
    public RegisterDtoValudator()
    {
        RuleFor(u => u.Fullname).MaximumLength(150);
        RuleFor(u => u.email).EmailAddress().NotEmpty().NotNull().MaximumLength(255);
        RuleFor(u => u.Username).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(u => u.password).NotEmpty().NotNull().MaximumLength(150);
    }
}
