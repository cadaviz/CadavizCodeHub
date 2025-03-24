using AutoFixture.Kernel;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CadavizCodeHub.Tests.Shared.Fixtures
{
    public class OverridePropertyBuilder<T, TProp> : ISpecimenBuilder
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly TProp _value;

        public OverridePropertyBuilder(Expression<Func<T, TProp>> expr, TProp value)
        {
            ArgumentNullException.ThrowIfNull(expr, nameof(expr));
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            _propertyInfo = (expr.Body as MemberExpression)?.Member as PropertyInfo ??
                            throw new InvalidOperationException("invalid property expression");
            _value = value;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;
            if (pi == null)
                return new NoSpecimen();

#pragma warning disable SYSLIB1045
            var camelCase = Regex.Replace(_propertyInfo.Name, @"(\w)(.*)", m => m.Groups[1].Value.ToLower() + m.Groups[2]);
#pragma warning restore SYSLIB1045

            if (pi.ParameterType != typeof(TProp) || pi.Name != camelCase)
                return new NoSpecimen();

            return _value!;
        }
    }
}
