using Fiorella.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
=======
using System.Linq.Expressions;
>>>>>>> 09f7faa9331bbd56949a343901d26f760b4ff139
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Abstraction.Repostiory
{
<<<<<<< HEAD
	public interface IRepository<T> where T : BaseEntity, new()
	{
		public DbSet<T> Table { get; }
	}
=======
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get;}

    }
>>>>>>> 09f7faa9331bbd56949a343901d26f760b4ff139
}
