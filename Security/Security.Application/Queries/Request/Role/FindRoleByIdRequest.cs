using MediatR;
using Security.Application.Queries.Response.Role;
using Security.Core.Wrappers;

namespace Security.Application.Queries.Request.Role
{
    public class FindRoleByIdRequest(string id) : IRequest<ApiResponse<FindRoleResponse>>
    {
        public string Id { get; private set; } = id;
    }
}
