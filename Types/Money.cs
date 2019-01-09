using Resort.Types.Units;

namespace Resort.Types
{
    class Money : UnitValue
    {
        public Money(int value) : base(value, Credits.Instance)
        { }

        public static Money operator +(Money left, Money right)
        {
            return new Money(left.Value + right.Value);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left.Value - right.Value);
        }

        public static Money operator *(Money left, int right)
        {
            return new Money(left.Value * right);
        }

        public static Money operator *(int left, Money right)
        {
            return right * left;
        }

        public static bool operator <(Money left, Money right)
        {
            return left.Value < right.Value;
        }

        public static bool operator <=(Money left, Money right)
        {
            return left.Value <= right.Value;
        }

        public static bool operator >(Money left, Money right)
        {
            return left.Value > right.Value;
        }

        public static bool operator >=(Money left, Money right)
        {
            return left.Value >= right.Value;
        }
    }
}
