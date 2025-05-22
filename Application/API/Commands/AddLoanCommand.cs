using Application.DTO;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddLoanCommand : IRequest<LoanDTO>
{
    public int BookId { get; set; }
}

public class AddLoanCommandHandler : IRequestHandler<AddLoanCommand, LoanDTO>
{
    private readonly IFakeLibraryUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddLoanCommandHandler(IFakeLibraryUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LoanDTO> Handle(AddLoanCommand command, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetSingle(book => book.Id == command.BookId, cancellationToken);

        // TODO: Handle invalid book id

        var loan = new Loan
        {
            LoanDate = DateTime.Now,
            BookId = command.BookId,
        };

        await _unitOfWork.Loans.Add(loan, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return _mapper.Map<LoanDTO>(loan);
    }
}
