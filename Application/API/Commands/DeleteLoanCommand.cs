using Application.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Commands;

public class DeleteLoanCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand>
{
    private readonly IFakeLibraryUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteLoanCommandHandler(IFakeLibraryUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(DeleteLoanCommand command, CancellationToken cancellationToken)
    {
        var loan = await _unitOfWork.Loans.GetSingle(loan => loan.Id == command.Id, cancellationToken);

        await _unitOfWork.Loans.Delete(loan, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
