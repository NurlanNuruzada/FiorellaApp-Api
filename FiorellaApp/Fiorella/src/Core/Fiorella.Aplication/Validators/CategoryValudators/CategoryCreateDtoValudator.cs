﻿using Fiorella.Aplication.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Validators.CategoryValudators
{
    public class CategoryCreateDtoValudator : AbstractValidator<CategoryGetDto>
    {
        public CategoryCreateDtoValudator()
        {
            RuleFor(x => x.name).MaximumLength(30).NotEmpty().NotNull();
            RuleFor(x => x.description).MaximumLength(500);
        }
    }
}
