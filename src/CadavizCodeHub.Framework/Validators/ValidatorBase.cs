using FluentValidation;
using FluentValidation.Results;
using System.Runtime.CompilerServices;

namespace CadavizCodeHub.Framework.Validators
{
    public class ValidatorBase<T> : AbstractValidator<T>
    {
        public ValidatorBase() : base() { }

        public ValidationResult Validate(T instance, [CallerArgumentExpression(nameof(instance))] string? paramName = null)
        {
            paramName ??= nameof(instance);

            return instance == null
                ? new ValidationResult([new ValidationFailure(paramName, $"The {paramName} cannot be null.")])
                : base.Validate(instance);
        }
    }
}
