using MediatR;
using Security.Application.Commands.Request.Account;
using Security.Application.Commands.Response.Account;
using Security.Core.Configs;
using Security.Core.Entities;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, ApiResponse<InputAccountResponse>>
    {
        private readonly IAccountRepository _repository;
        private readonly ILoggerService _loggerService;

        public CreateAccountHandler(IAccountRepository repository, ILoggerService loggerService)
        {
            _repository = repository;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<InputAccountResponse>> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";
            const string OPERATION = "criar conta";
            try
            {
                if (await _repository.IsExists(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MessageError.Conflito($"{OBJECT} {request.Name}"));
                    return ApiResponse<InputAccountResponse>.Error(MessageError.Conflito($"{OBJECT}"));
                }
                if (await _repository.IsEmailExists(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MessageError.ConflitoEmail(request.Email));
                    return ApiResponse<InputAccountResponse>.Error(MessageError.ConflitoEmail(request.Email));
                }

                var newAccount = new Accounts
                {
                    Name = request.Name,
                    Email = request.Email,
                    IdUser = request.IdUser,
                    UserName = request.Email,
                    PhoneNumber = request.Phone,
                    CreatedAt = DateTime.Now,
                    EndDate = DateTime.Parse(request.EndDate),
                    StartDate = DateTime.Parse(request.StartDate),
                    EmailConfirmed = true,
                    IsDeleted = false
                };

                var account = await _repository.Create(newAccount, request.Password, request.Role, cancellationToken);

                var result = new InputAccountResponse(account.Id, account.Name, account.Email!, account.CreatedAt.ToShortDateString());
                _loggerService.LogInformation(MessageError.OperacaoSucesso($"{OBJECT} {account.Name}", OPERATION));
                return ApiResponse<InputAccountResponse>.Success(result, MessageError.OperacaoSucesso(OBJECT, OPERATION));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MessageError.OperacaoErro(OBJECT, OPERATION), ex);
                return ApiResponse<InputAccountResponse>.Error(MessageError.OperacaoErro(OBJECT, OPERATION));
            }
        }
    }
}
