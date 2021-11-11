using FluentValidation;

namespace Aplication.Features.Categorias.Commands.CreateCategoriaCommand
{
    internal class CreateCategoriaCommandValidator : AbstractValidator<CreateCategoriaCommand>
    {
        public CreateCategoriaCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.")
                .MaximumLength(80).WithMessage("El {PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Descripcion)
                .NotEmpty().WithMessage("La {PropertyName} no puede estar vacía.")
                .MaximumLength(80).WithMessage("La {PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.")
                .MaximumLength(80).WithMessage("El {PropertyName} no debe exceder de {MaxLength} caracteres.");

        }
    }
}
