using Chamtsere.Domain.Entities.Appointment;
using Chamtsere.Domain.Entities.Service;
using Chamtsere.Domain.Entities.Tenant;
using Chamtsere.Domain.Entities.Token;
using Chamtsere.Domain.Entities.Transaction;
using Chamtsere.Domain.Entities.User.Customer;
using Chamtsere.Domain.Entities.User.Staff;

namespace Chamtsere.Application.Common.Interfaces;

public interface IChamtsereDbContext
{
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Token> Tokens { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}