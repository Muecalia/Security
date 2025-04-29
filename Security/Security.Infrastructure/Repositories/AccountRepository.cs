using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.Core.Entities;
using Security.Core.Repositories;
using System.Text;

namespace Security.Infrastructure.Repositories
{
    public class AccountRepository(UserManager<Accounts> userManager) : IAccountRepository
    {
        public async Task ChangePassword(Accounts account, string oldPassword, string newPassword, CancellationToken cancellationToken)
        {
            await userManager.ChangePasswordAsync(account, oldPassword, newPassword);
        }

        public async Task<Accounts> Create(Accounts account, string password, string role, CancellationToken cancellationToken)
        {
            account.CreatedAt = DateTime.Now;
            await userManager.CreateAsync(account, password);

            await userManager.AddToRoleAsync(account, role);
            return account;
        }

        public async Task Delete(Accounts account, CancellationToken cancellationToken)
        {
            account.IsDeleted = true;
            account.DeletedAt = DateTime.Now;
            await userManager.UpdateAsync(account);
        }

        public async Task<Accounts?> FindAccount(string id, CancellationToken cancellationToken)
        {
            return await userManager.Users.FirstOrDefaultAsync(u => !u.IsDeleted && string.Equals(u.Id, id), cancellationToken);
        }

        public async Task<List<Accounts>> FindAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await userManager.Users
                .AsNoTracking()
                .Where(u => !u.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Accounts?> FindById(string id, CancellationToken cancellationToken) => await userManager.Users.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id, cancellationToken);

        public async Task<Accounts?> FindByVolunteer(string idUser, CancellationToken cancellationToken) => await userManager.Users.FirstOrDefaultAsync(u => !u.IsDeleted && u.IdUser == idUser, cancellationToken);

        public async Task<bool> IsEmailExists(string email, CancellationToken cancellationToken)
        {
            return await userManager.Users.AnyAsync(u => string.Equals(u.Email, email), cancellationToken);
        }

        public async Task<bool> IsExists(string name, CancellationToken cancellationToken) => await userManager.Users.AnyAsync(u => u.Name.Equals(name), cancellationToken);

        public async Task Update(Accounts account, CancellationToken cancellationToken)
        {
            account.UpdatedAt = DateTime.Now;
            await userManager.UpdateAsync(account);
        }

        public async Task<string> GetRoles(Accounts account, CancellationToken cancellationToken)
        {
            var sb = new StringBuilder();
            var roles = await userManager.GetRolesAsync(account);
            foreach (var item in roles)
            {
                sb.Append(item);
                if (item != roles.LastOrDefault())
                    sb.Append(',');
            }
            return sb.ToString();
        }

    }
}
