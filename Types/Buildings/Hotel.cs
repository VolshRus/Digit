using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class Hotel : BuildingType
    {
        public static Hotel Instance { get; } = new Hotel();

        private Hotel() : base(
            title: "Гостиница",
            cost: new UnitValue(110_000, Credits.Instance),
            upkeep: new UnitValue(0, Credits.Instance),
            serviceType: NeedType.Room,
            servicesData: new UnitValue(60, Visitor.Instance))
        { }
    }
}
