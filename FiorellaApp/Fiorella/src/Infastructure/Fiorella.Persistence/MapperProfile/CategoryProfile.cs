using AutoMapper;
using Fiorella.Aplication.DTOs;
using Fiorella.Domain.Entities;

namespace Fiorella.Persistence.MapperProfile;

public class CategoryProfile:Profile
{
    public CategoryProfile()
    {
        CreateMap<Category,CategoryCreateDto>().ReverseMap();
        CreateMap<Category,CategoryGetDto>().ReverseMap();
        CreateMap<Category,CategoryUpdateDto>().ReverseMap();
    }
}
