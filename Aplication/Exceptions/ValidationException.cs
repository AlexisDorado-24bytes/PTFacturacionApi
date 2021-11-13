using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Aplication.Exceptions
{
    //Clase para la personalización de excepciones, con este canalizador se validaran las reglas del negocio al enviar peticiones
    public class ValidationException : Exception
    {
        public ValidationException() : base("Se han producido uno o mas errores de validacion.")
        {

            Errors = new List<string>();
        }

        public List<string> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
