using Resort.Types;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Buildings
{
    class NoviceTrack:Building
    {
        public override string Title => _title;
        public override Money Cost => _cost;

        public NoviceTrack() : base(_servicesData)
        { }

        private static string _title = $"Трасса для {Novices.Instance.TitleParrental}";
        private static Money _cost = new Money(30_000);
        private static (int, Visitor)[] _servicesData =
        {
            (20, Novices.Instance)
        };
    }
}
