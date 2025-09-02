using MediatR;
using Security.Application.Queries.Request.Role;
using Security.Application.Queries.Response.Role;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Role
{
    public class FindAllRolesHandler(IRoleRepository repository, ILoggerService loggerService) : IRequestHandler<FindAllRolesRequest, PagedResponse<FindRoleResponse>>
    {
        public async Task<PagedResponse<FindRoleResponse>> Handle(FindAllRolesRequest request, CancellationToken cancellationToken)
        {
            const string Title = "perfil";

            try
            {
                var results = new List<FindRoleResponse>();
                var roles = await repository.GetAll(request.PageNumber, request.PageSize, cancellationToken);

                roles.ForEach(role => results.Add(new FindRoleResponse(role.Id, role.Name!)));

                loggerService.LogInformation(MessageError.CarregamentoSucesso(Title));
                return PagedResponse<FindRoleResponse>.Success(results, request.PageNumber, request.PageSize, results.Count, MessageError.CarregamentoSucesso(Title, roles.Count));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.CarregamentoErro(Title, ex.Message));
                return PagedResponse<FindRoleResponse>.Error(MessageError.CarregamentoErro(Title));
            }
        }
    }
}
