using Microsoft.AspNetCore.Identity;

namespace Security.Core.Entities
{
    public class Accounts : IdentityUser
    {
        public required string Name { get; set; }
        public required string IdUser { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
