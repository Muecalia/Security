using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.Core.Repositories;

namespace Security.Infrastructure.Repositories
{
    public class RoleRepository(RoleManager<IdentityRole> roleManager) : IRoleRepository
    {
        public async Task<IdentityRole> Create(string name)
        {
            var role = new IdentityRole(name);
            await roleManager.CreateAsync(role);
            return role;
        }

        public async Task<bool> Delete(IdentityRole role)
        {
            var result = await roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> Exists(string role, CancellationToken cancellationToken) => await roleManager.Roles.AnyAsync(r => string.Equals(r.Name, role), cancellationToken);

        public async Task<List<IdentityRole>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await roleManager.Roles
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IdentityRole?> GetById(string id, CancellationToken cancellationToken) => await roleManager.Roles.FirstOrDefaultAsync(r => string.Equals(r.Id, id), cancellationToken);

        public async Task<bool> Update(IdentityRole role)
        {
            var result = await roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

    }
}
