using FluentValidation;
using Security.Application.Commands.Request.Role;

namespace Security.Application.Validators.Role
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleValidator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage("Informa o código")
                .NotEmpty().WithMessage("O código não pode estar vazio");

            RuleFor(r => r.Name)
                .NotNull().WithMessage("Informe o perfil")
                .NotEmpty().WithMessage("O perfil não pode estar vazio")
                .MinimumLength(4).WithMessage("O nome deve ter no mínimo 4 digitos");
        }
    }
}
