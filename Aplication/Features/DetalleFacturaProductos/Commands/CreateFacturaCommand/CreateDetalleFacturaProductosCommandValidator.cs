using Aplication.Features.DetalleFacturaProductos.Commands.CreateFacturaCommand;
using FluentValidation;

//Validamos los campos recibidos con FluentValidation
namespace Aplication.Features.Categorias.Commands.CreateCategoriaCommand
{
    public class CreateDetalleFacturaProductosCommandValidator : AbstractValidator<CreateDetalleFacturaProductosCommand>
    {
        public CreateDetalleFacturaProductosCommandValidator()
        {

            RuleFor(p => p.ProductoId)
                .NotEmpty().WithMessage("EL {PropertyName} no puede estar vacío.");


            RuleFor(p => p.Cantidad)
                .NotEmpty().WithMessage("La {PropertyName} no puede estar vacío.");

            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.");
        }


    }
}

