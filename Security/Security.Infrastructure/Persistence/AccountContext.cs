using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Security.Core.Entities;

namespace Security.Infrastructure.Persistence
{
    public class AccountContext(DbContextOptions<AccountContext> options) : IdentityDbContext<Accounts>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Accounts>(e =>
            {
                e.Property(a => a.Name).IsRequired().HasMaxLength(100);
                e.Property(a => a.Email).IsRequired().HasMaxLength(100);
                e.Property(a => a.IdUser).IsRequired().HasMaxLength(100);
                e.Property(a => a.IdGroup).IsRequired();
                e.Property(a => a.Group).IsRequired().HasMaxLength(100);
                e.Property(a => a.IsActive).HasDefaultValue(true);
                e.Property(a => a.IsDeleted).HasDefaultValue(false);

                e.HasIndex(a => a.IdUser);
                e.HasIndex(a => a.Name);
                e.HasIndex(a => a.Email);
                e.HasIndex(a => a.IsActive);
                e.HasIndex(a => a.IdGroup);
                e.HasIndex(a => a.Group);
                e.HasIndex(a => a.IsDeleted);
                e.HasIndex(a => a.CreatedAt);
                e.HasIndex(a => a.StartDate);
                e.HasIndex(a => a.EndDate);
            });

            base.OnModelCreating(builder);
        }
    }
}
