using Resort.Types;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Buildings
{
    class Chairlift:Building
    {
        public override string Title => _title;
        public override Money Cost => _cost;

        public Chairlift() : base(_servicesData)
        { }

        private static string _title = $"Подъемник кресельный";
        private static Money _cost = new Money(80_000);
        private static (int, Visitor)[] _servicesData =
        {
            (30, Visitor.Instance)
        };
    }
}
