using Chamtsere.Application.Common.Interfaces;
using Chamtsere.Domain.Entities.Appointment;
using Chamtsere.Domain.Entities.Service;
using Chamtsere.Domain.Entities.StaffProfile;
using Chamtsere.Domain.Entities.Tenant;
using Chamtsere.Domain.Entities.Transaction;
using Chamtsere.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Chamtsere.Infrastructure.Data;

public class ChamtsereDbContext : DbContext, IChamtsereDbContext
{
    private readonly ICurrentUser _currentUser;

    public ChamtsereDbContext(
        DbContextOptions<ChamtsereDbContext> options,
        ICurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser;
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<StaffProfile> StaffProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Apply global query filters for multi-tenancy
        var tenantId = Guid.TryParse(_currentUser.TenantId, out var id) ? id : Guid.Empty;
        builder.Entity<Appointment>()
        .HasQueryFilter(a => a.TenantId == tenantId);
    }
}
