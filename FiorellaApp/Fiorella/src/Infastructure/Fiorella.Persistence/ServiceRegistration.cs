using Fiorella.Aplication.Abstraction.Repostiory;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.Validators.CategoryValudators;
using Fiorella.Persistence.Contexts;
using Fiorella.Persistence.Helpers;
using Fiorella.Persistence.Inplementations.Repositories;
using Fiorella.Persistence.Inplementations.Services;
using Fiorella.Persistence.MapperProfile;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Fiorella.Persistence;
public static class ServiceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.ConnetionString);
        });

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining(typeof(CategoryCreateDtoValudator));
        services.AddAutoMapper(typeof(CategoryProfile).Assembly);


        services.AddReadARepositories();
        services.AddWriteARepositories();

        services.AddScoped<ICategoryService, CategoryService>();
    }
    private static void AddReadARepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
    }
    private static void AddWriteARepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
    }
}
