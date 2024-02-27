using Domain.Loans;

namespace Infrastructure.Repositories;

internal class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Loan loan)
    {
        _context.Add(loan);
    }
}
