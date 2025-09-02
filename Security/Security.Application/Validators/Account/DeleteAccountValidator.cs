using FluentValidation;
using Security.Application.Commands.Request.Account;

namespace Security.Application.Validators.Account
{
    public class DeleteAccountValidator : AbstractValidator<DeleteAccountRequest>
    {
        public DeleteAccountValidator()
        {
            RuleFor(u => u.Id)
                .NotNull().WithMessage("Informe o código do utilizador")
                .NotEmpty().WithMessage("O código do utilizador não pode estar vazio")
                .Length(36).WithMessage("Formato do código inválido");
        }
    }
}
