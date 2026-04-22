using FluentValidation;

namespace WeeTestIA.Application.Features.Perfil.Commands.DeleteCatPefil;

public class DeleteCatPerfilCommandValidator : AbstractValidator<DeleteCatPerfilCommand>
{
    public DeleteCatPerfilCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id del es obligatorio.");
    }
}
