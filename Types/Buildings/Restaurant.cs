using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class Restaurant : BuildingType
    {
        public static Restaurant Instance { get; } = new Restaurant();

        private Restaurant() : base(
            title: "Ресторан",
            cost: new UnitValue(80_000, Credits.Instance),
            upkeep: new UnitValue(0, Credits.Instance),
            serviceType: NeedType.Food,
            servicesData: new UnitValue(45, Visitor.Instance))
        { }
    }
}
