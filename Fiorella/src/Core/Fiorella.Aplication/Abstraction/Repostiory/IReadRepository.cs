using Fiorella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Abstraction.Repostiory
{
<<<<<<< HEAD
	public interface IReadRepository<T> : IRepository<T> where T : BaseEntity, new()
	{

		IQueryable<T> GetAll(bool isTracking = true, params string[] includes);
		IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression, int take, int skip, bool isTracking = true, params string[] includes);
		IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression, int take, int skip, Expression<Func<T, object>> expressionOrder, bool isordered = true, bool isTracking = true, params string[] includes);
		Task<T> GetByIdAsync(Guid id);
		Task<T> GetByExpression(Expression<Func<T, bool>> expression, bool isTracking = true);
	}
=======
    public interface IReadRepository<T>:IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(bool IsTracking = true, params string[] includes);
        IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression, int take, int Skip, bool IsTracking = true, params string[] includes);
        IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression, int take, int Skip, Expression<Func<T, object>> expressionOrder, bool IsOrder = true, bool IsTracking = true, params string[] includes);
        Task<T?> GetByIdAsync(int Id);
        Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool IsTracking = true);
    }
>>>>>>> 09f7faa9331bbd56949a343901d26f760b4ff139
}
