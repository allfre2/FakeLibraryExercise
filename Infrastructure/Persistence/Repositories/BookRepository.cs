using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BookRepository : EFRepository<Book>, IBookRepository
{
    public BookRepository(DbContext context) : base(context) { }
}
