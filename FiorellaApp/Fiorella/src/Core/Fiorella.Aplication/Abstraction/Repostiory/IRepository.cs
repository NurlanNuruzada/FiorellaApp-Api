using Fiorella.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Abstraction.Repostiory
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get;}

    }
}
