using Fiorella.API.Middlewares;
using Fiorella.Persistence;
using Fiorella.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Fiorella.Infrastucture;
using System.Globalization;

using Azure.Storage.Blobs;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Persistence.Inplementations.Services;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddLocalization();
List<CultureInfo> cultures = new() {
    new CultureInfo("es-ES"),
    new CultureInfo("eN-US"),
    new CultureInfo("ru-RU"),
};
RequestLocalizationOptions localizationOptions = new()
{
    ApplyCurrentCultureToResponseHeaders = true,
    SupportedCultures = cultures,
    SupportedUICultures = cultures
};
localizationOptions.SetDefaultCulture("en-US");
builder.Services.AddPersistanceServices();
builder.Services.AddInfrastuctureServices();
builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.AddScoped(_ =>
{
    return new BlobServiceClient(builder.Configuration["ConnetionStringAzure:AzureBlobStorage"]);
});

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:securityKey"])),
        LifetimeValidator = (_, expire, _, _) => expire > DateTime.UtcNow,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseRequestLocalization(localizationOptions);


using (var scope = app.Services.CreateScope())
{
    var instance = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await instance.InitializerAsync();
    await instance.RoleSeedAsync();
    await instance.UserSeedAsync();
}
app.UseCustomExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
