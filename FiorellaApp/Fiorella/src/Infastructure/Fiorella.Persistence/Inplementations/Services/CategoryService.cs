using AutoMapper;
using Fiorella.Aplication.Abstraction.Repostiory;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fiorella.Persistence.Inplementations.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _readRepository;

    private readonly ICategoryWriteRepository _writeRepository;
	private readonly IMapper _mapper;
	public CategoryService(ICategoryReadRepository readRepository,
                           ICategoryWriteRepository writeRepository,
                           IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CategoryGetDto categoryCreateDto)
    {
        Category? DBcategory =await _readRepository.
            GetByExpressionAsync(c => c.Name.ToLower().Equals(categoryCreateDto.name.ToLower()));
        if (DBcategory is not null)
        {
            throw new DublicatedException("Dublicated name!");
        }

    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        Category? categoryDb= await _readRepository.GetByIdAsync(id);
        if (categoryDb is null) throw new NotFoundException("Category not Found!");
        return _mapper.Map<CategoryGetDto>(categoryDb);
       
    }

    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _readRepository.GetAll().ToListAsync();
        List<CategoryGetDto> list= _mapper.Map<List<CategoryGetDto>>(categories);
        return list;
    }
}
