using FluentValidation;
using Security.Application.Commands.Request.Account;

namespace Security.Application.Validators.Account
{
    public class UpdateAccountValidator : AbstractValidator<UpdateAccountRequest>
    {
        public UpdateAccountValidator()
        {
            RuleFor(u => u.Id)
                .NotNull().WithMessage("Informe o código da conta")
                .NotEmpty().WithMessage("O código da conta não pode estar vazio")
                .Length(36).WithMessage("Formato do código inválido");

            RuleFor(u => u.Name)
                .NotNull().WithMessage("Informe o nome")
                .NotEmpty().WithMessage("O nome não pode estar vazio")
                .Length(2, 100).WithMessage("O tamanho dos caracteres do nome tem de estar entre 2 - 100");

            RuleFor(u => u.Email)
                .NotNull().WithMessage("Informe o e-mail")
                .NotEmpty().WithMessage("O e-mail não pode estar vazio")
                .EmailAddress().WithMessage("O formato do e-mail não é válido");

            RuleFor(u => u.Phone)
                .NotNull().WithMessage("Informe o telefone")
                .NotEmpty().WithMessage("O telefone não pode estar vazio")
                .Length(9, 20).WithMessage("O tamanho dos caracteres do telefone tem que estar de 9 - 20");
        }
    }
}
