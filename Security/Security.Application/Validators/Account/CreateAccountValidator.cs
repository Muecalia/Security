using FluentValidation;
using Security.Application.Commands.Request.Account;

namespace Security.Application.Validators.Account
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountValidator()
        {
            RuleFor(u => u.Name)
                .NotNull().WithMessage("Insira o nome")
                .NotEmpty().WithMessage("O nome não pode estar vazio")
                .Length(2, 100).WithMessage("O tamanho dos caracteres do nome tem de estar entre 2 - 100");

            RuleFor(u => u.Email)
                .NotNull().WithMessage("Insira o email")
                .NotEmpty().WithMessage("O e-mail não pode estar vazio")
                .EmailAddress().WithMessage("O formato do e-mail não é válido");

            RuleFor(u => u.Password)
                .NotNull().WithMessage("Insira a senha")
                .NotEmpty().WithMessage("A senha não pode estar vazia")
                .Length(4, 20).WithMessage("O tamanho dos caracteres na senha tem que estar de 4 - 20");

            RuleFor(u => u.Phone)
                .NotNull().WithMessage("Insira o telefone")
                .NotEmpty().WithMessage("O telefone não pode estar vazio")
                .Length(9, 20).WithMessage("O tamanho dos caracteres do telefone tem que estar de 9 - 20");

            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("Informa o perfil")
                .NotEmpty().WithMessage("O perfil não pode estar vazio");

            RuleFor(u => u.StartDate)
                .NotNull().WithMessage("Informa a data de inicio do mandato")
                .NotEmpty().WithMessage("A data de inicio do mandato não pode estar vazio")
                .Must(CustomValidators.IsValidDate).WithMessage("Data de inicio inválido");

            RuleFor(u => u.EndDate)
                .NotNull().WithMessage("Informa a data de termino do mandato")
                .NotEmpty().WithMessage("A data de termino do mandato não pode estar vazio")
                .Must(CustomValidators.IsValidDate).WithMessage("Data de termino inválido")
                .GreaterThan(item => item.StartDate).WithMessage("A data de termino do mandato deve ser maior ou igual que a data de início");
        }
    }
}
