using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class Cabinlift : BuildingType
    {
        public static Cabinlift Instance { get; } = new Cabinlift();

        private Cabinlift() : base(
           title: "Подъемник кабиночный",
           cost: new UnitValue(110_000, Credits.Instance),
           upkeep: new UnitValue(0, Credits.Instance),
           serviceType: NeedType.Elevator,
           servicesData: new UnitValue(50, Visitor.Instance))
        { }
    }
}
