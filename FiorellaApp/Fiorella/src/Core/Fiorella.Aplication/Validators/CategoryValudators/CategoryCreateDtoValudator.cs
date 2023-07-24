using Fiorella.Aplication.DTOs.CategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Validators.CategoryValudators
{
    public class CategoryCreateDtoValudator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValudator()
        {
            RuleFor(x => x.name).MaximumLength(30).NotEmpty().NotNull();
            RuleFor(x => x.description).MaximumLength(500);
        }
    }
}
