using FluentValidation;

//Validamos los campos recibidos con FluentValidation
namespace Aplication.Features.Productos.Commands.CreateProductosCommand
{
    public class CreatedProductoCommandValidator : AbstractValidator<CreatedProductoCommand>
    {
        public CreatedProductoCommandValidator()
        {

            RuleFor(p => p.CategoriaProductoId)
                .NotEmpty().WithMessage("EL {PropertyName} no puede estar vacío.");


            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("La {PropertyName} no puede estar vacío.");

            RuleFor(p => p.Descripcion)
                .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.");

            RuleFor(p => p.PrecioVentaCliente)
               .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.");

            RuleFor(p => p.PrecioCompra)
               .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.");

            RuleFor(p => p.Nombre)
               .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.");
        }


    }
}

