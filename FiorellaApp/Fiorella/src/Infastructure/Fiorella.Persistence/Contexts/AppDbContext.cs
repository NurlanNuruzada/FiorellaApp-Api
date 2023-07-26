using Fiorella.Domain.Entities;
using Fiorella.Persistence.Configurations;
using Fiorella.Persistence.Interseptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Persistence.Contexts;

public class AppDbContext :IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
    }
    public DbSet<Category> Categories { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Add the interceptor to the DbContext
        optionsBuilder.AddInterceptors(new DateModifiedInterseptors());
        base.OnConfiguring(optionsBuilder);
    }
}
