using Fiorella.Aplication.Abstraction.Repostiory;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Exceptions;

namespace Fiorella.Persistence.Inplementations.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _readRepository;

    private readonly ICategoryWriteRepository _writeRepository;
    public CategoryService(ICategoryReadRepository readRepository,
                           ICategoryWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task CreateAsync(CategoryCreateDto categoryCreateDto)
    {
        Category? DBcategory =await _readRepository.
            GetByExpressionAsync(c => c.Name.ToLower().Equals(categoryCreateDto.name.ToLower()));
        if (DBcategory is not null)
        {
            throw new DublicatedException("Dublicated name!");
        }

    }

    public Task<CategoryGetDto> DetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<CategoryGetDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
