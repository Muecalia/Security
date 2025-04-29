using MediatR;
using Security.Application.Commands.Response.Account;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Account
{
    public class CreateAccountRequest : IRequest<ApiResponse<InputAccountResponse>>
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Role { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string IdUser { get; set; }
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
    }
}
