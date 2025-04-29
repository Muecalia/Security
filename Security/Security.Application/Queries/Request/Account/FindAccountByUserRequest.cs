using MediatR;
using Security.Application.Queries.Response.Account;
using Security.Core.Wrappers;

namespace Security.Application.Queries.Request.Account
{
    public class FindAccountByUserRequest(string idVolunteer) : IRequest<ApiResponse<FindAccountResponse>>
    {
        public string IdVolunteer { get; set; } = idVolunteer;
    }
}
