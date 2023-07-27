using Fiorella.Aplication.Abstraction.Repostiory;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.Validators.CategoryValudators;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Contexts;
using Fiorella.Persistence.Helpers;
using Fiorella.Persistence.Inplementations.Repositories;
using Fiorella.Persistence.Inplementations.Services;
using Fiorella.Persistence.MapperProfile;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        //AppUser
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
        })
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AppDbContext>();

        // fluent valudation
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining(typeof(CategoryCreateDtoValudator));

        //mapper
        services.AddAutoMapper(typeof(CategoryProfile).Assembly);

        //repositories
        services.AddReadARepositories();
        services.AddWriteARepositories();

        //services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthService, AuthService>();

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
