using MediatR;
using Serilog;
using Security.Application.Commands.Request.Role;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Role
{
    public class CreateRoleHandler(IRoleRepository repository, ILoggerService loggerService) : IRequestHandler<RoleCreateRequest, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(RoleCreateRequest request, CancellationToken cancellationToken)
        {
            const string entidade = "perfil";
            const string operacao = "criar";
            try
            {
                if (await repository.Exists(request.Name, cancellationToken))
                {
                    Log.Warning(MessageError.Conflito($"{entidade} {request.Name}"));
                    return ApiResponse<string>.Error(MessageError.Conflito(entidade));
                }

                var result = await repository.Create(request.Name);

                var response = $"Perfil {result.Name!}";

                loggerService.LogInformation(MessageError.OperacaoSucesso(entidade, operacao));
                return ApiResponse<string>.Success(response, MessageError.OperacaoSucesso(entidade, operacao));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.OperacaoErro(entidade, operacao, ex.Message));
                return ApiResponse<string>.Error(MessageError.OperacaoErro(entidade, operacao));
            }
        }
    }
}
