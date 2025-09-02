using MediatR;
using Security.Application.Queries.Response.Account;
using Security.Core.Wrappers;

namespace Security.Application.Queries.Request.Account
{
    public class FindAllAccountsRequest(int pageNumber, int pageSize) : IRequest<PagedResponse<FindAllAccountsResponse>>
    {
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
    }
}
