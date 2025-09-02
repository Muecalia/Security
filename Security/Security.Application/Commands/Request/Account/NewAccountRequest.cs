using MediatR;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Account
{
    public class NewAccountRequest : IRequest<ApiResponse<string>>
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Role { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string IdUser { get; set; }
    }
}
