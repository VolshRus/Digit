using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class Hotel : BuildType
    {
        public static Hotel Instance { get; } = new Hotel();

        private Hotel() : base(
            title: "Гостиница",
            cost: new Money(110_000),
            upkeep: new Money(0),
            serviceType: NeedType.Room,
            clientsData: new ClientsAmount(60))
        { }
    }
}
