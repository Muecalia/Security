using MediatR;
using Security.Application.Commands.Request.Login;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Login
{
    public class LogoutUserHandler(ILoggerService loggerService, IAccountRepository repository) : IRequestHandler<LogoutUserRequest, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(LogoutUserRequest request, CancellationToken cancellationToken)
        {
            const string entidade = "user";
            const string operacao = "terminar sessão";
            try
            {
                var user = await repository.FindById(request.Id, cancellationToken);
                if (user is null)
                {
                    loggerService.LogWarning(MessageError.NotFound(entidade, request.Id));
                    return ApiResponse<string>.Error(MessageError.NotFound(entidade));
                }

                await repository.RemoveAuthenticationToken(user, cancellationToken);

                var result = $"Usuário {user.Name}.";
                var message = "Logout realizado com sucesso";
                loggerService.LogInformation(MessageError.OperacaoSucesso($"{entidade} {request.Id}", operacao));
                return ApiResponse<string>.Success(result, message);
                //return ApiResponse<string>.Success(result, MessageError.OperacaoSucesso(entidade, operacao));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.OperacaoErro(entidade, operacao, ex.Message));
                return ApiResponse<string>.Error("Erro ao sair da aplicação");
            }
        }
    }
}
