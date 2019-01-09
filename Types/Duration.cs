using System;
using Resort.Types.Units;

namespace Resort.Types
{
    class Duration : UnitValue
    {
        public Duration(int value) : base(value, Turns.Instance)
        { }
    }
}
