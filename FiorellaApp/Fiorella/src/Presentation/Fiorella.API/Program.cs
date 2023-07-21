using Fiorella.Aplication.Abstraction.Repostiory;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.Validators.CategoryValudators;
using Fiorella.Persistence.Contexts;
using Fiorella.Persistence.Inplementations.Repositories;
using Fiorella.Persistence.Inplementations.Services;
using Fiorella.Persistence.MapperProfile;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CategoryCreateDtoValudator));
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
builder.Services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
