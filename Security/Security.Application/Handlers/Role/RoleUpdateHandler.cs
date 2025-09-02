using MediatR;
using Security.Application.Commands.Request.Role;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Role
{
    public class RoleUpdateHandler(IRoleRepository repository, ILoggerService loggerService) : IRequestHandler<RoleUpdateRequest, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(RoleUpdateRequest request, CancellationToken cancellationToken)
        {
            const string entidade = "perfil";
            const string operacao = "atualizar";
            try
            {
                var role = await repository.GetById(request.Id, cancellationToken);
                if (role is null)
                {
                    loggerService.LogWarning(MessageError.NotFound(entidade, request.Id));
                    return ApiResponse<string>.Error(MessageError.NotFound(operacao));
                }

                role.Name = request.Name;
                await repository.Update(role);
                string response = $"Perfil {role.Name}";
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
