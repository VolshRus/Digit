using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class Cabinlift : BuildType
    {
        public static Cabinlift Instance { get; } = new Cabinlift();

        private Cabinlift() : base(
           title: "Подъемник кабиночный",
           cost: new Money(110_000),
           upkeep: new Money(0),
           serviceType: NeedType.Elevator,
           clientsData: new ClientsAmount(50))
        { }
    }
}
