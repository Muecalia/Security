using MediatR;
using Security.Application.Commands.Request.Account;
using Security.Core.Configs;
using Security.Core.Entities;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Account
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, ApiResponse<string>>
    {
        private readonly IAccountRepository _repository;
        private readonly ILoggerService _loggerService;

        public CreateAccountHandler(IAccountRepository repository, ILoggerService loggerService)
        {
            _repository = repository;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<string>> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "conta";
            const string OPERATION = "criar conta";
            try
            {
                if (await _repository.IsExists(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MessageError.Conflito($"{OBJECT} {request.Name}"));
                    return ApiResponse<string>.Error(MessageError.Conflito($"{OBJECT}"));
                }
                if (await _repository.IsEmailExists(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MessageError.ConflitoEmail(request.Email));
                    return ApiResponse<string>.Error(MessageError.ConflitoEmail(request.Email));
                }

                var newAccount = new Accounts
                {
                    Name = request.Name,
                    Email = request.Email,
                    UserName = request.Email,
                    PhoneNumber = request.Phone,
                    CreatedAt = DateTime.Now,
                    EndDate = DateTime.Parse(request.EndDate),
                    StartDate = DateTime.Parse(request.StartDate),
                    Group = request.Group,
                    EmailConfirmed = true,
                    IsDeleted = false
                };

                _ = Guid.TryParse(string.IsNullOrEmpty(request.IdUser) ? string.Empty : request.IdUser, out Guid idUser);
                newAccount.IdUser = idUser;

                _ = Guid.TryParse(string.IsNullOrEmpty(request.IdGroup) ? string.Empty : request.IdGroup, out Guid idGroup);
                newAccount.IdGroup = idGroup;

                var account = await _repository.Create(newAccount, request.Password, request.Role, cancellationToken);

                var result = $"User {newAccount.Name}";
                _loggerService.LogInformation(MessageError.OperacaoSucesso($"{OBJECT} {account.Name}", OPERATION));
                return ApiResponse<string>.Success(result, MessageError.OperacaoSucesso(OBJECT, OPERATION));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MessageError.OperacaoErro(OBJECT, OPERATION), ex);
                return ApiResponse<string>.Error(MessageError.OperacaoErro(OBJECT, OPERATION));
            }
        }
    }
}
