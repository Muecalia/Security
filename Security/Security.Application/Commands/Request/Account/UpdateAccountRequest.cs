using MediatR;
using Security.Core.Wrappers;

namespace Security.Application.Commands.Request.Account
{
    public class UpdateAccountRequest : IRequest<ApiResponse<string>>
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Group { get; set; }
        public required string IdGroup { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
    }
}
