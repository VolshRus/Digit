using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class NoviceTrack : BuildType
    {
        public static NoviceTrack Instance { get; } = new NoviceTrack();

        private NoviceTrack() : base(
            title: $"Трасса для {Novice.Instance.TitleParrental}",
            cost: new Money(30_000),
            upkeep: new Money(10_000),
            serviceType: NeedType.Track,
            clientsData: new NovicesAmount(20))
        { }
    }
}
