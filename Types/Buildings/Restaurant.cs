using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class Restaurant : BuildType
    {
        public static Restaurant Instance { get; } = new Restaurant();

        private Restaurant() : base(
            title: "Ресторан",
            cost: new Money(80_000),
            upkeep: new Money(0),
            serviceType: NeedType.Food,
            clientsData: new ClientsAmount(45))
        { }
    }
}
