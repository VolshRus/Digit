using Resort.Types.Units;
using System;

namespace Resort.Types
{
    abstract class UnitValue    {        public int Value { get; private set; }        public Unit Unit { get; private set; }        public UnitValue(int value, Unit unit)        {            Value = value;            Unit = unit;        }
        public override string ToString()
        {
            return $"{Value} {Unit.ShortTitleParrental}";        }
        public static implicit operator string(UnitValue input)        {            return $"{input.Value} {input.Unit.ShortTitleParrental}";        }
    }
}
