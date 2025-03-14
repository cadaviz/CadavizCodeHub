using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CadavizCodeHub.Core.Shared.Validators
{
    public class ValidatorBase<T> : AbstractValidator<T> 
    {
        public ValidatorBase() : base() { }

        public ValidationResult Validate(T instance, [CallerArgumentExpression(nameof(instance))] string? paramName = null)
        {
            paramName ??= nameof(instance);

            return EqualityComparer<T>.Default.Equals(instance, default)
                ? new ValidationResult([new ValidationFailure(paramName, $"The {paramName} cannot be null.")])
                : base.Validate(instance);
        }
    }
}
