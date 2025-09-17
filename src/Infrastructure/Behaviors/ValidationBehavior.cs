using FluentValidation;
using Mediator;

namespace TodoApp.Infrastructure.Behaviors;
public class ValidationBehavior<TMessage, TResponse>(IEnumerable<IValidator<TMessage>> validators) : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TMessage>(message);
            var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        return await next(message, cancellationToken);
    }
}
