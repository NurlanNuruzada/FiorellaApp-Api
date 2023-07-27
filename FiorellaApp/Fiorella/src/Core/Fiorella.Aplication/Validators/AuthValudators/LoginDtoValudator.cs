using Fiorella.Aplication.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Validators.AuthValudators
{
    public class LoginDtoValudator : AbstractValidator<SingInDto>
    {
        public LoginDtoValudator()
        {
            RuleFor(u => u.UserOrEmail).NotEmpty().NotNull().MaximumLength(255);
            RuleFor(u=>u.password).NotEmpty().NotNull().MaximumLength(150);
        }
    }
}
