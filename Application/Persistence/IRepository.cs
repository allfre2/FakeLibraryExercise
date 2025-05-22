using System.Linq.Expressions;

namespace Application.Persistence;

public interface IRepository<T> where T : class
{
    Task<T> GetSingle(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
    Task Add(T entity, CancellationToken cancellationToken);
    Task AddRange(IEnumerable<T> items, CancellationToken cancellationToken);
    Task Update(T entity, CancellationToken cancellationToken);
    Task Delete(T entity, CancellationToken cancellationToken);
    Task DeleteRange(IEnumerable<T> entities, CancellationToken cancellationToken);
}
