using MediatR;
using Security.Application.Commands.Request.Login;
using Security.Application.Commands.Response.Login;
using Security.Core.Configs;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Core.Wrappers;

namespace Security.Application.Handlers.Login
{
    public class LoginUserHandler(IJwtTokenService jwtTokenService, ILoggerService loggerService, IAccountRepository accountRepository, ILoginRepository loginRepository) : IRequestHandler<LoginUserRequest, ApiResponse<LoginUserResponse>>
    {
        public async Task<ApiResponse<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            const string entidade = "user";
            const string operacao = "logar";
            try
            {
                var user = await loginRepository.FindByEmail(request.Email, cancellationToken);
                if (user is null)
                {
                    loggerService.LogWarning(MessageError.NotFoundEmail(entidade, request.Email));
                    return ApiResponse<LoginUserResponse>.Error(MessageError.NotFound(entidade));
                }

                var role = await accountRepository.GetRoles(user, cancellationToken);

                var resultLogin = await loginRepository.SignInUser(request.Email, request.Password);
                if (!resultLogin.Succeeded)
                {
                    loggerService.LogWarning(MessageError.SenhaErrada(user.Name));
                    return ApiResponse<LoginUserResponse>.Error(MessageError.SenhaErrada());
                }

                var token = jwtTokenService.GenerateJwtToken(user.Name, user.Email!, role);

                if (token is null)
                {
                    loggerService.LogWarning(MessageError.NotFoundEmail("token", request.Email));
                    return ApiResponse<LoginUserResponse>.Error(MessageError.NotFound("token"));
                }

                var result = new LoginUserResponse(user.Id, user.Name, user.Email!, token, role);

                loggerService.LogInformation(MessageError.OperacaoSucesso($"{entidade} {request.Email}", operacao));
                return ApiResponse<LoginUserResponse>.Success(result, MessageError.OperacaoSucesso(entidade, operacao));
            }
            catch (Exception ex)
            {
                loggerService.LogError(MessageError.OperacaoErro(entidade, operacao, ex.Message));
                return ApiResponse<LoginUserResponse>.Error(MessageError.OperacaoErro(entidade, operacao));
            }
        }
    }
}
