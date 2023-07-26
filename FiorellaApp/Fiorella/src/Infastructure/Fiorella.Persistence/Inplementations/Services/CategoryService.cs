using AutoMapper;
using Fiorella.Aplication.Abstraction.Repostiory;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.CategoryDTOs;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fiorella.Persistence.Inplementations.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly IMapper _mapper;

    private readonly ICategoryWriteRepository _writeRepository;
    public CategoryService(ICategoryReadRepository readRepository,
                           ICategoryWriteRepository writeRepository,
                           IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CategoryCreateDto categoryCreateDto)
    {
        Category? DBcategory = await _readRepository.
            GetByExpressionAsync(c => c.Name.ToLower().Equals(categoryCreateDto.name.ToLower()));
        if (DBcategory is not null)
        {
            throw new DublicatedException("Dublicated name!");
        }
        Category newCategory = _mapper.Map<Category>(categoryCreateDto);
        await _writeRepository.addAsync(newCategory);
        await _writeRepository.SaveChangesAsync();
    }

    public async Task<CategoryGetDto> getById(Guid id)
    {
        Category? category = await _readRepository.GetByIdAsync(id);
        CategoryGetDto categoryGetDto = _mapper.Map<CategoryGetDto>(category);
        if (category is null)
        {
            throw new NotFoundException("Not found!!!");
        }
        else
        {
            return categoryGetDto;
        }
    }
    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _readRepository.GetAll().ToListAsync();
        List<CategoryGetDto> List = _mapper.Map<List<CategoryGetDto>>(categories);
        return List;
    }

    public async Task Remove(Guid id) 
    {
        Category foundCategory = await _readRepository.GetByIdAsync(id);
        if (foundCategory is null)
        {
            throw new NotFoundException("Not found!!!");
        }
        _writeRepository.remove(foundCategory);
        await _writeRepository.SaveChangesAsync();
    }
    public async Task UpdateAsync(CategoryUpdateDto categoryUpdateDto,Guid Id)
    {
        var category = await _readRepository.GetByIdAsync(Id);
        if (category is null)
        {
            throw new NotFoundException("Not found!!!");
        }
        _mapper.Map(categoryUpdateDto, category);
        await _writeRepository.SaveChangesAsync();
    }
}
