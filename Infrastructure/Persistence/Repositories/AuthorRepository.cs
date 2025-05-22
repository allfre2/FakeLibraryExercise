using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AuthorRepository : EFRepository<Author>, IAuthorRepository
{
    public AuthorRepository(DbContext context) : base(context) { }
}
