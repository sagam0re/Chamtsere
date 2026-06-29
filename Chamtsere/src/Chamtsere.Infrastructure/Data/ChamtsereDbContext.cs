using Chamtsere.Application.Common.Interfaces;
using Chamtsere.Domain.Entities.Booking;
using Chamtsere.Domain.Entities.SalonService;
using Chamtsere.Domain.Entities.Token;
using Chamtsere.Domain.Entities.Transaction;
using Chamtsere.Domain.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Chamtsere.Infrastructure.Data;

public class ChamtsereDbContext : IdentityDbContext<ApplicationUser>, IChamtsereDbContext
{
    public ChamtsereDbContext(DbContextOptions<ChamtsereDbContext> options) : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<SalonService> SalonServices { get; set; }
    public DbSet<Token> Tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
