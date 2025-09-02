using FluentValidation;
using Security.Application.Commands.Request.Login;

namespace Security.Application.Validators.Login
{
    public class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(l => l.Email)
                .NotNull().WithMessage("Informa o e-mail")
                .NotEmpty().WithMessage("O e-mail não pode estar vazio")
                .EmailAddress().WithMessage("Formato do email inválido");

            RuleFor(l => l.Password)
                .NotNull().WithMessage("Informa a senha")
                .NotEmpty().WithMessage("A senha não pode estar vazia");
        }
    }
}
