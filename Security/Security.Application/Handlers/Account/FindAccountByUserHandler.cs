using MediatR;
using Security.Application.Queries.Request.Account;
using Security.Application.Queries.Response.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class FindAccountByUserHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<FindAccountByUserRequest, ApiResponse<FindAccountResponse>>
    {
        public async Task<ApiResponse<FindAccountResponse>> Handle(FindAccountByUserRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";

            try
            {
                var account = await repository.FindByVolunteer(request.IdVolunteer, cancellationToken);
                if (account == null)
                {
                    loggerService.LogWarning(MessageError.NotFound(OBJECT, request.IdVolunteer));
                    return ApiResponse<FindAccountResponse>.Error(MessageError.NotFound(OBJECT));
                }

                var roles = await repository.GetRoles(account, cancellationToken);
                var result = new FindAccountResponse(account.Id, account.Name, account.Email!, account.PhoneNumber!, roles ?? string.Empty, account.CreatedAt.ToShortDateString());

                loggerService.LogInformation(MessageError.CarregamentoSucesso($"{OBJECT} {account.Name}"));
                return ApiResponse<FindAccountResponse>.Success(result, MessageError.CarregamentoSucesso(OBJECT));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.CarregamentoErro(OBJECT), ex);
                return ApiResponse<FindAccountResponse>.Error(MessageError.CarregamentoErro(OBJECT));
            }
        }
    }
}
