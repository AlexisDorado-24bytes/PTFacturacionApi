using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new FluentValidation.ValidationContext<TRequest>(request);
                var validatorResult = await Task.WhenAll(_validators.Select(d => d.ValidateAsync(context, cancellationToken)));
                var failures = validatorResult.SelectMany(e => e.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    throw new Exceptions.ValidationException(failures);
                }

            }
            return await next();
        }
    }
}
