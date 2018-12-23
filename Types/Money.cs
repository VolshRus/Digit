using Resort.Types.Units;

namespace Resort.Types
{
    internal class Money : UnitValue    {        public Money(int value) : base(value, Credits.Instance)        { }        public static UnitValue operator +(Money left, Money right)        {            return new Money(left.Value + right.Value);        }    }
}
