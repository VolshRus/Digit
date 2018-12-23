using Resort.Types;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Buildings
{
    class ProTrack:Building
    {
        public override string Title => _title;
        public override Money Cost => _cost;

        public ProTrack() : base(_servicesData)
        { }

        private static string _title = $"Трасса для {Pro.Instance.TitleParrental}";
        private static Money _cost = new Money(60_000);
        private static (int, Visitor)[] _servicesData =
        {
            (20, Pro.Instance)
        };
    }
}
