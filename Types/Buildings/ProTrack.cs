using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class ProTrack : BuildingType
    {
        public static ProTrack Instance { get; } = new ProTrack();

        private ProTrack() : base(
            title: $"Трасса для {Pro.Instance.TitleParrental}",
            cost: new UnitValue(60_000, Credits.Instance),
            upkeep: new UnitValue(20_000, Credits.Instance),
            serviceType: NeedType.Track,
            servicesData: new UnitValue(20, Pro.Instance))
        { }
    }
}
