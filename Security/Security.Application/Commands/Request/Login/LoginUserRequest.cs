using MediatR;
using Security.Application.Commands.Response.Login;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Login
{
    public class LoginUserRequest : IRequest<ApiResponse<LoginUserResponse>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
