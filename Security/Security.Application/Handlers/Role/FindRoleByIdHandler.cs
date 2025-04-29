using MediatR;
using Security.Application.Queries.Request.Role;
using Security.Application.Queries.Response.Role;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Role
{
    public class FindRoleByIdHandler(IRoleRepository repository, ILoggerService loggerService) : IRequestHandler<FindRoleByIdRequest, ApiResponse<FindRoleResponse>>
    {
        public async Task<ApiResponse<FindRoleResponse>> Handle(FindRoleByIdRequest request, CancellationToken cancellationToken)
        {
            const string Entidade = "perfil";

            try
            {
                var role = await repository.GetById(request.Id, cancellationToken);
                if (role == null)
                    return ApiResponse<FindRoleResponse>.Error(MessageError.NotFound(Entidade));

                var result = new FindRoleResponse(role.Id, role.Name!);
                loggerService.LogInformation(MessageError.CarregamentoSucesso(Entidade));
                return ApiResponse<FindRoleResponse>.Success(result, MessageError.CarregamentoSucesso(Entidade));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.CarregamentoErro(Entidade, ex.Message));
                return ApiResponse<FindRoleResponse>.Error(MessageError.CarregamentoErro(Entidade));
                //throw;
            }
        }
    }
}
