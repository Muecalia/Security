using MediatR;
using Security.Application.Commands.Response.Account;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Account
{
    public class UpdateAccountRequest : IRequest<ApiResponse<InputAccountResponse>>
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
