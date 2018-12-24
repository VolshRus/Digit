using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class Chairlift : BuildingType
    {
        public static Chairlift Instance { get; } = new Chairlift();

        private Chairlift() : base(
            title: "Подъемник кресельный",
            cost: new UnitValue(80_000, Credits.Instance),
            upkeep: new UnitValue(0, Credits.Instance),
            serviceType: NeedType.Elevator,
            servicesData: new UnitValue(30, Visitor.Instance))
        { }
    }
}
