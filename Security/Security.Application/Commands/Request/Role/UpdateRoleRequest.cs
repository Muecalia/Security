using MediatR;
using Security.Application.Commands.Response.Role;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Role
{
    public class UpdateRoleRequest : IRequest<ApiResponse<InputRoleResponse>>
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
    }
}
