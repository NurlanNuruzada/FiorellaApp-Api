using Fiorella.Aplication.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Abstraction.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryGetDto> getById(Guid id);
        Task<List<CategoryGetDto>> GetAllAsync();
        Task UpdateAsync(CategoryUpdateDto categoryUpdateDto,Guid id);
        Task Remove(Guid id);
    }
}
