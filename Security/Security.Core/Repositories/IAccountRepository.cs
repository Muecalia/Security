using Security.Core.Entities;

namespace Security.Core.Repositories
{
    public interface IAccountRepository
    {
        Task<Accounts> Create(Accounts account, string password, string role, CancellationToken cancellationToken);
        Task Update(Accounts account, CancellationToken cancellationToken);
        Task ChangePassword(Accounts account, string oldPassword, string newPassword, CancellationToken cancellationToken);
        Task Delete(Accounts account, CancellationToken cancellationToken);
        Task<string> GetRoles(Accounts user, CancellationToken cancellationToken);
        Task<List<Accounts>> FindAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<Accounts?> FindAccount(string id, CancellationToken cancellationToken);
        Task<Accounts?> FindById(string id, CancellationToken cancellationToken);
        Task<Accounts?> FindByVolunteer(string idVolunteer, CancellationToken cancellationToken);
        Task<bool> IsEmailExists(string email, CancellationToken cancellationToken);
        Task<bool> IsExists(string name, CancellationToken cancellationToken);
    }
}
