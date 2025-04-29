using FluentValidation;
using Security.Application.Commands.Request.Role;

namespace Security.Application.Validators.Role
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleRequest>
    {
        public DeleteRoleValidator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage("Informa o código")
                .NotEmpty().WithMessage("O código não pode estar vazio");
        }
    }
}
