using Application.DTO;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddBookCommand : IRequest<BookDTO>
{
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }

    public int AuthorId { get; set; }
}

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, BookDTO>
{
    private readonly IFakeLibraryUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddBookCommandHandler(IFakeLibraryUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookDTO> Handle(AddBookCommand command, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetSingle(author => author.Id == command.AuthorId, cancellationToken);

        // TODO: Handle author not found

        var book = new Book
        {
            Title = command.Title,
            PublicationDate = command.PublicationDate,
            Genre = command.Genre,
            AuthorId = command.AuthorId,
        };

        await _unitOfWork.Books.Add(book, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return _mapper.Map<BookDTO>(book);
    }
}
