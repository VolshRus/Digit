using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class Chairlift : BuildType
    {
        public static Chairlift Instance { get; } = new Chairlift();

        private Chairlift() : base(
            title: "Подъемник кресельный",
            cost: new Money(80_000),
            upkeep: new Money(0),
            serviceType: NeedType.Elevator,
            clientsData: new ClientsAmount(30))
        { }
    }
}
