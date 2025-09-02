using Microsoft.AspNetCore.Identity;

namespace Security.Core.Repositories
{
    public interface ILoginRepository
    {
        Task<SignInResult> SignInUser(string email, string password);
    }
}
