using MediatR;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Login
{
    public class LogoutUserRequest(string id) : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; } = id;
    }
}
