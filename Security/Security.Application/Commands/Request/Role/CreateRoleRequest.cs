using MediatR;
using Security.Application.Commands.Response.Role;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Role
{
    public class CreateRoleRequest : IRequest<ApiResponse<InputRoleResponse>>
    {
        public required string Name { get; set; }
    }
}
