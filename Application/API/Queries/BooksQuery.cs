using Application.DTO;
using Application.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Queries;

public class BooksQuery : IRequest<IEnumerable<BookDTO>>
{
    public string? Title { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Genre { get; set; }
    public int? AuthorId { get; set; }
}

public class BooksQueryHandler : IRequestHandler<BooksQuery, IEnumerable<BookDTO>>
{
    private readonly IFakeLibraryUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BooksQueryHandler(IFakeLibraryUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDTO>> Handle(BooksQuery query, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books.GetAll(book =>
            (string.IsNullOrEmpty(query.Title) || book.Title == query.Title)
            && (query.From == null || book.PublicationDate >= query.From)
            && (query.To == null || book.PublicationDate <= query.To)
            && (string.IsNullOrEmpty(query.Genre) || book.Genre == query.Genre)
            && (query.AuthorId == null || book.AuthorId == query.AuthorId), cancellationToken);

        return _mapper.Map<IEnumerable<BookDTO>>(books);
    }
}
