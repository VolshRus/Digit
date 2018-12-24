using Resort.Types.Units;
using System;

namespace Resort.Types.Units
{
    internal class UnitValue    {        public int Value { get; private set; }        public Unit Unit { get; private set; }        public UnitValue(int value, Unit unit)        {            Value = value;            Unit = unit;        }        public override string ToString()
        {
            return $"{Value} {Unit.ShortTitleParrental}";        }        public static implicit operator string(UnitValue input)        {            return $"{input.Value} {input.Unit.ShortTitleParrental}";        }        public static UnitValue operator +(UnitValue left, UnitValue right)
        {
            if (left.Unit != right.Unit) throw new FormatException("Sum values with different units");
            return new UnitValue(left.Value + right.Value, left.Unit);
        }        public static UnitValue operator -(UnitValue left, UnitValue right)
        {
            if (left.Unit != right.Unit) throw new FormatException("Sum values with different units");
            return new UnitValue(left.Value - right.Value, left.Unit);
        }        public static UnitValue operator *(UnitValue left, int right)
        {
            return new UnitValue(left.Value * right, left.Unit);
        }        public static UnitValue operator *(int left, UnitValue right)
        {
            return right * left;
        }    }
}
