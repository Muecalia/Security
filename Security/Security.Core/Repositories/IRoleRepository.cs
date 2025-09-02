using Microsoft.AspNetCore.Identity;

namespace Security.Core.Repositories
{
    public interface IRoleRepository
    {
        Task<IdentityRole> Create(string name);
        Task<bool> Update(IdentityRole role);
        Task<bool> Delete(IdentityRole role);
        Task<bool> Exists(string role, CancellationToken cancellationToken);
        Task<IdentityRole?> GetById(string id, CancellationToken cancellationToken);
        Task<List<IdentityRole>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
