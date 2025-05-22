using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence;

public class EFRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;

    public EFRepository(DbContext context)
    {
        _context = context;
    }

    public async Task Add(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task AddRange(IEnumerable<T> items, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddRangeAsync(items);
    }

    public async Task Update(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task Delete(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task DeleteRange(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().Where(filterExpression).ToListAsync(cancellationToken);
    }

    public async Task<T> GetSingle(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().SingleAsync(filterExpression, cancellationToken);
    }
}
