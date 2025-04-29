using Microsoft.AspNetCore.Identity;
using Security.Core.Entities;

namespace Security.Core.Repositories
{
    public interface ILoginRepository
    {
        Task<SignInResult> SignInUser(string email, string password);
        Task<Accounts?> FindByEmail(string email, CancellationToken cancellationToken);
    }
}
