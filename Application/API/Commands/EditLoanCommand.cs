using Application.DTO;
using Application.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Commands;

public class EditLoanCommand : IRequest<LoanDTO>
{
    public int? Id { get; set; }
    public DateTime? ReturnDate { get; set; }
}

public class EditLoanCommandHandler : IRequestHandler<EditLoanCommand, LoanDTO>
{
    private readonly IFakeLibraryUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EditLoanCommandHandler(IFakeLibraryUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LoanDTO> Handle(EditLoanCommand command, CancellationToken cancellationToken)
    {
        var loan = await _unitOfWork.Loans.GetSingle(loan => loan.Id == command.Id, cancellationToken);

        // TODO: Handle invalid loan Id

        loan.ReturnDate = command.ReturnDate ?? DateTime.Now;

        await _unitOfWork.CompleteAsync(cancellationToken);

        return _mapper.Map<LoanDTO>(loan);
    }
}
