using MediatR;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Role
{
    public class RoleCreateRequest : IRequest<ApiResponse<string>>
    {
        public required string Name { get; set; }
    }
}
