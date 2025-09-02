using MediatR;
using Security.Application.Commands.Request.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class UpdateAccountHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<UpdateAccountRequest, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            const string entidade = "conta";
            const string operacao = "atualizar";
            try
            {
                var conta = await repository.FindAccount(request.Id, cancellationToken);
                if (conta == null)
                {
                    loggerService.LogWarning(MessageError.NotFound(entidade, request.Id));
                    return ApiResponse<string>.Error(MessageError.NotFound(entidade));
                }

                conta.Name = request.Name;
                conta.Email = request.Email;
                conta.Group = request.Group;
                //conta.IdGroup = request.IdGroup;
                conta.PhoneNumber = request.Phone;
                conta.StartDate = DateTime.Parse(request.StartDate);
                conta.EndDate = DateTime.Parse(request.EndDate);

                _ = Guid.TryParse(string.IsNullOrEmpty(request.IdGroup) ? string.Empty : request.IdGroup, out Guid idGroup);
                conta.IdGroup = idGroup;


                await repository.Update(conta, cancellationToken);
                var result = $"utilizador {conta.Name}";

                loggerService.LogInformation(MessageError.OperacaoSucesso($"{entidade} {conta.Name}", operacao));
                return ApiResponse<string>.Success(result, MessageError.OperacaoSucesso(entidade, operacao));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.OperacaoErro(entidade, operacao), ex);
                return ApiResponse<string>.Error(MessageError.OperacaoErro(entidade, operacao));
            }
        }
    }
}
