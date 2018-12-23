using Resort.Types;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Buildings
{
    class CommonTrack:Building
    {
        public override string Title => _title;
        public override Money Cost => _cost;

        public CommonTrack() : base(_servicesData)
        { }

        private static string _title = $"Обычная трасса";
        private static Money _cost = new Money(45_000);
        private static (int, Visitor)[] _servicesData =
        {
            (10, Novices.Instance),
            (10, Pro.Instance)
        };
    }
}
