namespace Application.Persistence;

public interface IFakeLibraryUnitOfWork : IUnitOfWork
{
    IAuthorRepository Authors { get; }
    IBookRepository Books { get; }
    ILoanRepository Loans { get; }
}
