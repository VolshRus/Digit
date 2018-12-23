using Resort.Types;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Buildings
{
    class Cabinlift:Building
    {
        public override string Title => _title;
        public override Money Cost => _cost;

        public Cabinlift() : base(_servicesData)
        { }

        private static string _title = $"Подъемник кабиночный";
        private static Money _cost = new Money(110_000);
        private static (int, Visitor)[] _servicesData =
        {
            (50, Visitor.Instance)
        };
    }
}
