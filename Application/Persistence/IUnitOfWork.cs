namespace Application.Persistence;

public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken);
}
