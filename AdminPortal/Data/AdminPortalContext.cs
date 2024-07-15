using Microsoft.EntityFrameworkCore;
using AdminPortal.Entities;

namespace AdminPortal.Data;

public class AdminPortalContext(DbContextOptions<AdminPortalContext> options) : DbContext(options)
{
    public DbSet<Claim> Claims => Set<Claim>();

    public DbSet<Tenant> Tenants => Set<Tenant>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<UserGroup> UserGroups => Set<UserGroup>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Claim>().HasData(
            new Claim { Id = 1, Name = "Read Contacts Claim" },
            new Claim { Id = 2, Name = "Write Contacts Claim" }
        );

        modelBuilder.Entity<Tenant>().HasData(
            new Claim { Id = 1, Name = "Google" },
            new Claim { Id = 2, Name = "Deloitte" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Manson", Tenants = [1, 2], UserGroups = [1] },
            new User { Id = 2, Name = "Ashwin", Tenants = [1], UserGroups = [2] }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Contacts Read Role", TenantId = 1, Claims = [1] },
            new Role { Id = 2, Name = "Contacts Admin Role", TenantId = 1, Claims = [1, 2] }
        );

        modelBuilder.Entity<UserGroup>().HasData(
            new UserGroup { Id = 1, Name = "Contacts Read Role", TenantId = 1, Roles = [1] },
            new UserGroup { Id = 2, Name = "Contacts Admin Role", TenantId = 1, Roles = [2] }
        );
    }
}