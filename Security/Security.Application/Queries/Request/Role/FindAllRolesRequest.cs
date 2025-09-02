using MediatR;
using Security.Application.Queries.Response.Role;
using Security.Core.Wrappers;

namespace Security.Application.Queries.Request.Role
{
    public class FindAllRolesRequest(int pageNumber, int pageSize) : IRequest<PagedResponse<FindRoleResponse>>
    {
        public int PageNumber { get; private set; } = pageNumber;
        public int PageSize { get; private set; } = pageSize;
    }
}
