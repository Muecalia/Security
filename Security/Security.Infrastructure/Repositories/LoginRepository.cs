using Microsoft.AspNetCore.Identity;
using Security.Core.Entities;
using Security.Core.Repositories;

namespace Security.Infrastructure.Repositories
{
    public class LoginRepository(SignInManager<Accounts> signInManager) : ILoginRepository
    {
        public async Task<SignInResult> SignInUser(string email, string password) => await signInManager.PasswordSignInAsync(email, password, false, false);
    }
}
