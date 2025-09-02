using MediatR;
using Security.Application.Queries.Request.Account;
using Security.Application.Queries.Response.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class FindAccountByGroupHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<FindAccountByGroupRequest, PagedResponse<FindAllAccountsResponse>>
    {
        public async Task<PagedResponse<FindAllAccountsResponse>> Handle(FindAccountByGroupRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";

            try
            {
                var results = new List<FindAllAccountsResponse>();
                var accounts = await repository.FindByGrupo(request.IdGroup, cancellationToken);
                foreach (var account in accounts)
                {
                    var roles = await repository.GetRoles(account, cancellationToken);
                    results.Add(new FindAllAccountsResponse(account.Id, account.Name, account.IdUser.ToString()!, account.Email!, account.PhoneNumber!, account.Group!, roles, account.StartDate.ToShortDateString(), account.EndDate.ToShortDateString(), account.CreatedAt.ToShortDateString()));
                }
                loggerService.LogInformation(MessageError.CarregamentoSucesso(OBJECT, accounts.Count));
                return PagedResponse<FindAllAccountsResponse>.Success(results, 1, results.Count, results.Count, MessageError.CarregamentoSucesso(OBJECT));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.CarregamentoErro(OBJECT), ex);
                return PagedResponse<FindAllAccountsResponse>.Error(MessageError.CarregamentoErro(OBJECT));
            }
        }
    }
}
