using MediatR;
using Security.Application.Commands.Request.Role;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Role
{
    public class DeleteRoleHandler(IRoleRepository repository, ILoggerService loggerService) : IRequestHandler<RoleDeleteRequest, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(RoleDeleteRequest request, CancellationToken cancellationToken)
        {
            const string entidade = "perfil";
            const string operacao = "eliminar";
            try
            {
                var role = await repository.GetById(request.Id, cancellationToken);
                if (role is null)
                    return ApiResponse<string>.Error(MessageError.NotFound(entidade));

                var result = await repository.Delete(role);
                if (!result)
                    return ApiResponse<string>.Error(MessageError.OperacaoErro(entidade, operacao));

                string response = $"Nome {role.Name}";
                loggerService.LogInformation(MessageError.OperacaoSucesso(entidade, operacao));
                return ApiResponse<string>.Success(response, MessageError.OperacaoSucesso(entidade, operacao));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.OperacaoErro(entidade, operacao, ex.Message));
                return ApiResponse<string>.Error(MessageError.OperacaoErro(entidade, operacao, ex.Message));
            }
        }
    }
}
