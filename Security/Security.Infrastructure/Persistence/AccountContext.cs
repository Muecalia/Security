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
                e.Property(a => a.IdUser).IsRequired().HasMaxLength(50);
                e.Property(a => a.IsActive).HasDefaultValue(true);
                e.Property(a => a.IsDeleted).HasDefaultValue(false);

                e.HasIndex(a => a.IdUser);
                e.HasIndex(a => a.Name).IsUnique();
                e.HasIndex(a => a.Email).IsUnique();
            });

            base.OnModelCreating(builder);
        }
    }
}
