using MediatR;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Account
{
    public class DeleteAccountRequest(string id) : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; } = id;
    }
}
