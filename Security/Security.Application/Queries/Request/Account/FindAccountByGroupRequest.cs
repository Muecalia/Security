using MediatR;
using Security.Application.Queries.Response.Account;
using Security.Core.Wrappers;

namespace Security.Application.Queries.Request.Account
{
    public class FindAccountByGroupRequest(string idGroup) : IRequest<PagedResponse<FindAllAccountsResponse>>
    {
        public string IdGroup { get; set; } = idGroup;
    }
}
