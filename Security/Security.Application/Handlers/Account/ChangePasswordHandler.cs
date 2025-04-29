using MediatR;
using Security.Application.Commands.Request.Account;
using Security.Application.Commands.Response.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class ChangePasswordHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<ChangePasswordRequest, ApiResponse<InputAccountResponse>>
    {
        public async Task<ApiResponse<InputAccountResponse>> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";
            const string OPERATION = "alterar senha";
            try
            {
                var account = await repository.FindById(request.Id, cancellationToken);
                if (account == null)
                {
                    loggerService.LogWarning(MessageError.NotFound(OBJECT, request.Id));
                    return ApiResponse<InputAccountResponse>.Error(MessageError.NotFound(OBJECT));
                }

                await repository.ChangePassword(account, request.OldPassword, request.NewPassword, cancellationToken);
                var result = new InputAccountResponse(account.Id, account.Name, account.Email!, account.CreatedAt.ToShortDateString());

                loggerService.LogInformation(MessageError.OperacaoSucesso($"{OBJECT} {account.Name}", OPERATION));
                return ApiResponse<InputAccountResponse>.Success(result, MessageError.OperacaoSucesso(OBJECT, OPERATION));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.OperacaoErro(OBJECT, OPERATION), ex);
                return ApiResponse<InputAccountResponse>.Error(MessageError.OperacaoErro(OBJECT, OPERATION));
            }
        }
    }
}
