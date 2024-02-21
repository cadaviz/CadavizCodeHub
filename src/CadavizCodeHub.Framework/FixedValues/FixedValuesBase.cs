using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadavizCodeHub.Framework.FixedValues
{
    //internal class FixedValuesBase
    //{
    //    public abstract class FixedValuesBase<TUnderlyingValue> : IFixedValue, IConvertible, IEquatable<FixedValuesBase<TUnderlyingValue>>
    //    {
    //        private readonly Type thisType;

    //        protected FixedValuesBase(TUnderlyingValue? value)
    //        {
    //            Value = value;
    //            Description = string.Empty; //GlobalizationHelper.GetText("{0}FixedValue{1}".With(GetType().Name, value));

    //            thisType = this.GetType();
    //        }

    //        public TUnderlyingValue? Value { get; private set; }
    //        public string Description { get; protected set; }
    //        //[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
    //        object? IFixedValue.ValueAsObject
    //        {
    //            get => Value;
    //        }

    //        public override string ToString()
    //        {
    //            return Value?.ToString();
    //        }

    //        public override bool Equals(object obj)
    //        {
    //            FixedValuesBase<TUnderlyingValue>? other = obj as FixedValuesBase<TUnderlyingValue>;

    //            if (other is null || thisType != other.GetType())
    //                return false;

    //            if (Value is null && other.Value is null)
    //                return true;

    //            return Value is not null && Value.Equals(other.Value);
    //        }

    //        public bool Equals(FixedValuesBase<TUnderlyingValue> other) => Equals((object)other);

    //        public override int GetHashCode() => (Value).GetValueOrDefault().GetHashCode();

    //        public TypeCode GetTypeCode()
    //        {
    //            var underlyingType = typeof(TUnderlyingValue);

    //            if (underlyingType == typeof(int))
    //            {
    //                return TypeCode.Int32;
    //            }

    //            return TypeCode.String;
    //        }

    //        public string? ToString(IFormatProvider provider)
    //        {
    //            return Value == null ? null : Convert.ToString(Value, provider);
    //        }

    //        public int ToInt32(IFormatProvider provider) => Convert.ToInt32(Value, provider);
    //        public short ToInt16(IFormatProvider provider) => Convert.ToInt16(Value, provider);
    //        public object ToType(Type conversionType, IFormatProvider provider)
    //        {
    //            if (conversionType == thisType)
    //            {
    //                return this;
    //            }

    //            // TODO: Utilizado pela serialização para JSON.
    //            return this.ToString(provider);
    //        }

    //        public bool ToBoolean(IFormatProvider provider) => throw new NotImplementedException();
    //        public char ToChar(IFormatProvider provider) => throw new NotImplementedException();
    //        public sbyte ToSByte(IFormatProvider provider) => throw new NotImplementedException();
    //        public byte ToByte(IFormatProvider provider) => throw new NotImplementedException();
    //        public ushort ToUInt16(IFormatProvider provider) => throw new NotImplementedException();
    //        public uint ToUInt32(IFormatProvider provider) => throw new NotImplementedException();
    //        public long ToInt64(IFormatProvider provider) => throw new NotImplementedException();
    //        public ulong ToUInt64(IFormatProvider provider) => throw new NotImplementedException();
    //        public float ToSingle(IFormatProvider provider) => throw new NotImplementedException();
    //        public double ToDouble(IFormatProvider provider) => throw new NotImplementedException();
    //        public decimal ToDecimal(IFormatProvider provider) => throw new NotImplementedException();
    //        public DateTime ToDateTime(IFormatProvider provider) => throw new NotImplementedException();
    //    }
    //}
}