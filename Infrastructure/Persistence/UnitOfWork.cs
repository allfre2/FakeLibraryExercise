using Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
