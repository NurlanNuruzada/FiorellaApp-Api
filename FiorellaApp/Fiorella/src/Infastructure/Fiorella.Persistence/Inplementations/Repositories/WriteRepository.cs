using Fiorella.Aplication.Abstraction.Repostiory;
using Fiorella.Domain.Entities.Common;
using Fiorella.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Persistence.Inplementations.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    public readonly AppDbContext _context;
    public DbSet<T> Table => _context.Set<T>();
    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task addAsync(T entity) =>   await Table.AddAsync(entity);
    public async Task addRangeAsync(ICollection<T> enitites)=>  await Table.AddRangeAsync(enitites);
    public  void remove(T entity)=>Table.Remove(entity);
    public void removeRange(ICollection<T> enitites)=>  Table.RemoveRange(enitites);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    void IWriteRepository<T>.update(T entity)=> Table.Update(entity);
       

}
