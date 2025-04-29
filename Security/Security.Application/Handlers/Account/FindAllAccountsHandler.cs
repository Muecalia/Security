using MediatR;
using Security.Application.Queries.Request.Account;
using Security.Application.Queries.Response.Account;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class FindAllAccountsHandler(IAccountRepository repository, ILoggerService loggerService) : IRequestHandler<FindAllAccountsRequest, PagedResponse<FindAllAccountsResponse>>
    {
        public async Task<PagedResponse<FindAllAccountsResponse>> Handle(FindAllAccountsRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";
            try
            {
                var results = new List<FindAllAccountsResponse>();
                var accounts = await repository.FindAll(request.PageNumber, request.PageSize, cancellationToken);

                results = accounts.Select(user => new FindAllAccountsResponse(user.Id, user.Name, user.Email!, user.PhoneNumber!, user.CreatedAt.ToShortDateString())).ToList();

                loggerService.LogInformation(MessageError.CarregamentoSucesso(OBJECT, accounts.Count));
                return PagedResponse<FindAllAccountsResponse>.Success(results, request.PageNumber, request.PageSize, results.Count, MessageError.CarregamentoSucesso(OBJECT));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.CarregamentoErro(OBJECT), ex);
                return PagedResponse<FindAllAccountsResponse>.Error(MessageError.CarregamentoErro(OBJECT));
            }
        }
    }
}
