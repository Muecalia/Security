using MediatR;
using Security.Application.Commands.Response.Account;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Account
{
    public class ChangePasswordRequest : IRequest<ApiResponse<string>>
    {
        public required string Id { get; set; }
        public required string NewPassword { get; set; }
        public required string OldPassword { get; set; }
    }
}
