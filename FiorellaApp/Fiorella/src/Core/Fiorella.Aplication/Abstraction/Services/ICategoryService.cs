using Fiorella.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Abstraction.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryGetDto categoryCreateDto);
        Task<CategoryGetDto> GetByIdAsync(int id);
        Task<List<CategoryGetDto>> GetAllAsync();
        //Task UpdateAsync(CategoryUpdateDto categoryUpdateDto);
    }
}
