using Application.DTO;
using Application.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Queries;

public class AuthorsQuery : IRequest<IEnumerable<AuthorDTO>>
{
}

public class AuthorsQueryHandler : IRequestHandler<AuthorsQuery, IEnumerable<AuthorDTO>>
{
    private readonly IFakeLibraryUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthorsQueryHandler(IFakeLibraryUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDTO>> Handle(AuthorsQuery query, CancellationToken cancellationToken)
    {
        var authors = await _unitOfWork.Authors.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
    }
}
