using FluentValidation;

namespace WeeTestIA.Application.Features.Perfil.Commands.CreateCatPerfil;

public class CreateCatPerfilCommandValidator : AbstractValidator<CreaCatPerfilCommand>
{
    public CreateCatPerfilCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El Nombre del Perfil es obligatorio.")
            .MaximumLength(50).WithMessage("El Nombre puede exceder los 50 caracteres.");
        RuleFor(x => x.Descripcion)
           .NotEmpty().WithMessage("El Nombre del Perfil es obligatorio.")
           .MaximumLength(250).WithMessage("El Nombre puede exceder los 250 caracteres.");
    }
}
