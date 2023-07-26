using Fiorella.API.Middlewares;
using Fiorella.Persistence;
using Fiorella.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddPersistanceServices();
builder.Services.AddScoped<AppDbContextInitializer>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var instance = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await instance.Inititalizer();
    await instance.RoleSeedAsync();
    await instance.UserSeedService();
}

app.UseCustomExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
