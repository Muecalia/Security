using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.Core.Entities;
using Security.Core.Repositories;

namespace Security.Infrastructure.Repositories
{
    public class LoginRepository(SignInManager<Accounts> signInManager, UserManager<Accounts> userManager) : ILoginRepository
    {
        public async Task<SignInResult> SignInUser(string email, string password) => await signInManager.PasswordSignInAsync(email, password, false, false);

        public async Task<Accounts?> FindByEmail(string email, CancellationToken cancellationToken)
        {
            return await userManager.Users.FirstOrDefaultAsync(u => !u.IsDeleted && string.Equals(u.Email, email), cancellationToken);
        }

    }
}
