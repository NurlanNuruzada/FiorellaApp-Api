using Fiorella.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Abstraction.Repostiory
{
    public interface IWriteRepository<T>:IRepository<T> where T : BaseEntity, new()
    {
        Task addAsync(T entity);
        Task addRangeAsync(ICollection<T> enitites);
        void update(T entity);
        void remove(T entity);
         void removeRange(ICollection<T> enitites);
        Task SaveChangesAsync();
    }
}
