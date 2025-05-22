using Application.DTO;
using Application.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Queries;

public class LoansQuery : IRequest<IEnumerable<LoanDTO>>
{
}

public class LoansQueryHandler : IRequestHandler<LoansQuery, IEnumerable<LoanDTO>>
{
    private readonly IFakeLibraryUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LoansQueryHandler(IFakeLibraryUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LoanDTO>> Handle(LoansQuery query, CancellationToken cancellationToken)
    {
        var loans = await _unitOfWork.Loans.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<LoanDTO>>(loans);
    }
}
