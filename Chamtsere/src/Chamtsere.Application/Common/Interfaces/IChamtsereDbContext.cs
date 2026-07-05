using Chamtsere.Domain.Entities.Appointment;
using Chamtsere.Domain.Entities.Service;
using Chamtsere.Domain.Entities.StaffProfile;
using Chamtsere.Domain.Entities.Tenant;
using Chamtsere.Domain.Entities.Transaction;
using Chamtsere.Domain.Entities.User;

namespace Chamtsere.Application.Common.Interfaces;

public interface IChamtsereDbContext
{
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<StaffProfile> StaffProfiles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}