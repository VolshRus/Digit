using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class ProTrack : BuildType
    {
        public static ProTrack Instance { get; } = new ProTrack();

        private ProTrack() : base(
            title: $"Трасса для {Pro.Instance.TitleParrental}",
            cost: new Money(60_000),
            upkeep: new Money(20_000),
            serviceType: NeedType.Track,
            clientsData: new ProAmount(20))
        { }
    }
}
