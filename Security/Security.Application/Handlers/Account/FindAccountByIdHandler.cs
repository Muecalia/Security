using MediatR;
using Security.Application.Queries.Request.Account;
using Security.Application.Queries.Response.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class FindAccountByIdHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<FindAccountByIdRequest, ApiResponse<FindAccountByIdResponse>>
    {
        public async Task<ApiResponse<FindAccountByIdResponse>> Handle(FindAccountByIdRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";

            try
            {
                var account = await repository.FindById(request.Id, cancellationToken);
                if (account == null)
                {
                    loggerService.LogWarning(MessageError.NotFound(OBJECT, request.Id));
                    return ApiResponse<FindAccountByIdResponse>.Error(MessageError.NotFound(OBJECT));
                }

                var roles = await repository.GetRoles(account, cancellationToken);
                var result = new FindAccountByIdResponse(account.Id, account.Name, account.IdUser.ToString()!, account.Email!, account.PhoneNumber!, account.Group!, account.IdGroup.ToString()!, roles, account.StartDate.ToShortDateString(), account.EndDate.ToShortDateString(), account.CreatedAt.ToShortDateString());

                loggerService.LogInformation(MessageError.CarregamentoSucesso($"{OBJECT} {account.Name}"));
                return ApiResponse<FindAccountByIdResponse>.Success(result, MessageError.CarregamentoSucesso(OBJECT));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.CarregamentoErro(OBJECT), ex);
                return ApiResponse<FindAccountByIdResponse>.Error(MessageError.CarregamentoErro(OBJECT));
            }
        }
    }
}
