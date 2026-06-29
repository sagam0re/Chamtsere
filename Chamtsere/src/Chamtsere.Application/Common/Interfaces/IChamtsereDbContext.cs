using Chamtsere.Domain.Entities.Booking;
using Chamtsere.Domain.Entities.SalonService;
using Chamtsere.Domain.Entities.Token;
using Chamtsere.Domain.Entities.Transaction;

namespace Chamtsere.Application.Common.Interfaces;

public interface IChamtsereDbContext
{
    DbSet<Booking> Bookings { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<SalonService> SalonServices { get; set; }
    DbSet<Token> Tokens { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}