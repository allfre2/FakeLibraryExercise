using Application.DTO;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddAuthorCommand : IRequest<AuthorDTO>
{
    public string Name { get; set; }
    public string Nationality { get; set; }
}

public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AuthorDTO>
{
    private readonly IFakeLibraryUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddAuthorCommandHandler(IFakeLibraryUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorDTO> Handle(AddAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Name = command.Name,
            Nationality = command.Nationality,
        };

        await _unitOfWork.Authors.Add(author, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return _mapper.Map<AuthorDTO>(author);
    }
}
