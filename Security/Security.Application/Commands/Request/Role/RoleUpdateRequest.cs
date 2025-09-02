using MediatR;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Role
{
    public class RoleUpdateRequest : IRequest<ApiResponse<string>>
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
    }
}
