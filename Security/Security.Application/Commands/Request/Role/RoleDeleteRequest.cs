using MediatR;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Role
{
    public class RoleDeleteRequest(string id) : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; } = id;
    }
}
