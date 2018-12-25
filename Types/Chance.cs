using System;
using Resort.Types.Units;

namespace Resort.Types
{
    class Chance : UnitValue
    {
        public Chance(int value) : base(value, Percents.Instance)
        { }

        public static Chance operator +(Chance left, Chance right)
        {
            return new Chance(left.Value + right.Value);
        }

        public static Chance operator -(Chance left, Chance right)
        {
            return new Chance(left.Value - right.Value);
        }

        public static Chance operator *(Chance left, int right)
        {
            return new Chance(left.Value * right);
        }

        public static Chance operator *(int left, Chance right)
        {
            return right * left;
        }
    }

}
