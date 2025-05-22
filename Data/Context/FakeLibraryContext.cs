using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class FakeLibraryContext : IdentityDbContext
{
    public FakeLibraryContext(DbContextOptions<FakeLibraryContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Author>()
            .HasIndex(author => author.Nationality);

        builder.Entity<Book>()
            .HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId);
        builder.Entity<Book>()
            .HasIndex(book => book.Genre);
        builder.Entity<Book>()
            .HasIndex(book => book.PublicationDate);

        builder.Entity<Loan>()
            .HasOne(loan => loan.Book)
            .WithMany(book => book.Loans)
            .HasForeignKey(loan => loan.BookId);
        builder.Entity<Loan>()
            .HasIndex(loan => loan.BookId);
        builder.Entity<Loan>()
            .HasIndex(loan => loan.LoanDate);

        base.OnModelCreating(builder);
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> books { get; set; }
    public DbSet<Loan> Loans { get; set; }
}
