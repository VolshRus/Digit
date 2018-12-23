using Resort.Types.Units;

namespace Resort.Types
{
    internal abstract class UnitValue    {        public int Value { get; private set; }        public Unit Unit { get; private set; }        protected UnitValue(int value, Unit unit)        {            Value = value;            Unit = unit;        }        public override string ToString()
        {
            return $"{Value} {Unit.ShortTitleParrental}";        }        public static implicit operator string(UnitValue input)        {            return $"{input.Value} {input.Unit.ShortTitleParrental}";        }    }
}
