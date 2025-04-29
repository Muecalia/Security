using MediatR;
using Security.Application.Commands.Request.Account;
using Security.Application.Commands.Response.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class UpdateAccountHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<UpdateAccountRequest, ApiResponse<InputAccountResponse>>
    {
        public async Task<ApiResponse<InputAccountResponse>> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            const string entidade = "conta";
            const string operacao = "atualizar conta";
            try
            {
                var account = await repository.FindAccount(request.Id, cancellationToken);
                if (account == null)
                {
                    loggerService.LogWarning(MessageError.NotFound(entidade, request.Id));
                    return ApiResponse<InputAccountResponse>.Error(MessageError.NotFound(entidade));
                }

                account.Name = request.Name;
                account.Email = request.Email;
                account.PhoneNumber = request.Phone;

                await repository.Update(account, cancellationToken);
                var result = new InputAccountResponse(account.Id, account.Name, account.Email, account.CreatedAt.ToShortDateString());

                loggerService.LogInformation(MessageError.OperacaoSucesso($"{entidade} {account.Name}", operacao));
                return ApiResponse<InputAccountResponse>.Success(result, MessageError.OperacaoSucesso(entidade, operacao));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.OperacaoErro(entidade, operacao), ex);
                return ApiResponse<InputAccountResponse>.Error(MessageError.OperacaoErro(entidade, operacao));
            }
        }
    }
}
