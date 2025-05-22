using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class LoanRepository : EFRepository<Loan>, ILoanRepository
{
    public LoanRepository(DbContext context) : base(context) { }
}
