using Application.Persistence;
using Data.Context;

namespace Infrastructure.Persistence;

public class FakeLibraryUnitOfWork : UnitOfWork, IFakeLibraryUnitOfWork
{
    private readonly AuthorRepository _authorRepository;
    private readonly BookRepository _bookRepository;
    private readonly LoanRepository _loanRepository;

    public FakeLibraryUnitOfWork(FakeLibraryContext context) : base(context)
    {
        _authorRepository = new AuthorRepository(context);
        _bookRepository = new BookRepository(context);
        _loanRepository = new LoanRepository(context);
    }

    public IAuthorRepository Authors => _authorRepository;
    public IBookRepository Books => _bookRepository;
    public ILoanRepository Loans => _loanRepository;
}
