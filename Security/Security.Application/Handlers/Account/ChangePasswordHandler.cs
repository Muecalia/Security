using MediatR;
using Security.Application.Commands.Request.Account;
using Security.Application.Commands.Response.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class ChangePasswordHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<ChangePasswordRequest, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";
            const string OPERATION = "alterar senha";
            try
            {
                var conta = await repository.FindById(request.Id, cancellationToken);
                if (conta == null)
                {
                    loggerService.LogWarning(MessageError.NotFound(OBJECT, request.Id));
                    return ApiResponse<string>.Error(MessageError.NotFound(OBJECT));
                }

                await repository.ChangePassword(conta, request.OldPassword, request.NewPassword, cancellationToken);
                var result = $"utilizador {conta.Name}";

                loggerService.LogInformation(MessageError.OperacaoSucesso($"{OBJECT} {conta.Name}", OPERATION));
                return ApiResponse<string>.Success(result, MessageError.OperacaoSucesso(OBJECT, OPERATION));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.OperacaoErro(OBJECT, OPERATION), ex);
                return ApiResponse<string>.Error(MessageError.OperacaoErro(OBJECT, OPERATION));
            }
        }
    }
}
