using Fiorella.Aplication.Abstraction.Repostiory;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Fiorella.Persistence.Inplementations.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    public readonly AppDbContext _context;
    public DbSet<T> Table => _context.Set<T>(); 
    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }
    public IQueryable<T> GetAll(bool IsTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        foreach (var include in includes)
        {
            query.Include(include);
        }
        return  IsTracking ? query : query.AsNoTracking();
    }
    public IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression,
                                          int take,
                                          int Skip,
                                          bool IsTracking = true,
                                          params string[] includes)
    {
        var query = Table.Where(expression).Skip(Skip).Take(take).AsQueryable();
        foreach (var include in includes)
        {
            query.Include(include);
        }
        return IsTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression,
                                                 int take,
                                                 int Skip,
                                                 Expression<Func<T, object>> expressionOrder,
                                                 bool IsOrder = true,
                                                 bool IsTracking = true,
                                                 params string[] includes)
    {
        var query = Table.Where(expression).AsQueryable();
        query = IsOrder ? query.OrderBy(expressionOrder) : query.OrderByDescending(expressionOrder);
        query = query.Skip(Skip).Take(take);
        foreach (var include in includes)
        {
            query.Include(include);
        }
        return IsTracking ? query : query.AsNoTracking();
    }


    public async Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool IsTracking = true)
    {
        var query = IsTracking ? Table.AsQueryable() : Table.AsNoTracking().AsQueryable();
        return await query.FirstOrDefaultAsync(expression);
    }
    public async Task<T?> GetByIdAsync(int Id) => await Table.FindAsync(Id);
}
