using MediatR;
using Security.Application.Commands.Response.Account;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Account
{
    public class DeleteAccountRequest(string id) : IRequest<ApiResponse<InputAccountResponse>>
    {
        public string Id { get; set; } = id;
    }
}
