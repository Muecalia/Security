using MediatR;
using Security.Application.Queries.Response.Account;
using Security.Core.Wrappers;

namespace Security.Application.Queries.Request.Account
{
    public class FindAccountByIdRequest(string id) : IRequest<ApiResponse<FindAccountResponse>>
    {
        public string Id { get; set; } = id;
    }
}
