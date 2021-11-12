using Aplication.Features.Facturas.Commands.CreateFacturaCommand;
using FluentValidation;

//Validamos los campos recibidos con FluentValidation
namespace Aplication.Features.Categorias.Commands.CreateCategoriaCommand
{
    public class CreateFacturaCommandValidator : AbstractValidator<CreateFacturaCommand>
    {
        public CreateFacturaCommandValidator()
        {

            RuleFor(p => p.FechaFactura)
                .NotEmpty().WithMessage("La {PropertyName} no puede estar vacío.");


            RuleFor(p => p.ClienteId)
                .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.");

            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("El {PropertyName} no puede estar vacío.");



        }


    }
}

