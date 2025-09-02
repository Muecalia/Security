using MediatR;
using Security.Application.Commands.Request.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class DeleteAccountHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<DeleteAccountRequest, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";
            const string OPERATION = "eliminar conta";
            try
            {
                var conta = await repository.FindById(request.Id, cancellationToken);
                if (conta == null)
                {
                    loggerService.LogWarning(MessageError.NotFound(OBJECT, request.Id));
                    return ApiResponse<string>.Error(MessageError.NotFound(OBJECT));
                }

                await repository.Delete(conta, cancellationToken);
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
