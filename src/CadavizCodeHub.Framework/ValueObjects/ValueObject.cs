using System;
using System.Collections.Generic;
using System.Reflection;

namespace CadavizCodeHub.Framework.ValueObjects
{
    public abstract class ValueObject<TValueObject> : IValueObject, IEquatable<TValueObject> where TValueObject : ValueObject<TValueObject>
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            TValueObject other = obj as TValueObject;

            return Equals(other);
        }

        public virtual bool Equals(TValueObject? other)
        {
            if (other is null)
                return false;

            Type type = GetType();
            Type otherType = other.GetType();

            if (type != otherType)
                return false;

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if ((value1 == null && value2 != null) || (value1 != null && !value1.Equals(value2)))
                    return false;
            }

            return true;
        }
        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int startValue = 17;
            int multiplier = 59;
            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }
        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();
            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
                t = t.BaseType;
            }

            return fields;
        }

        public static bool operator ==(ValueObject<TValueObject> x, ValueObject<TValueObject> y)
        {
            if (x is null)
                return true;

            return x.Equals(y);
        }

        public static bool operator !=(ValueObject<TValueObject> x, ValueObject<TValueObject> y)
        {
            return !(x == y);
        }
    }
}