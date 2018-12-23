using Resort.Types;
using Resort.Types.Units;

namespace Resort.Buildings
{
    internal class Hotel : Building
    {
        public override string Title => _title;
        public override Money Cost => _cost;

        public Hotel() : base(_servicesData)
        { }

        private static string _title = "Гостиница";
        private static Money _cost = new Money(110_000);
        private static (int, Visitor)[] _servicesData =
        {
            (60, Visitor.Instance)
        };
    }
}
