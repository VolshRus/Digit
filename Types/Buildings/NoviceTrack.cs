using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class NoviceTrack : BuildingType
    {
        public static NoviceTrack Instance { get; } = new NoviceTrack();

        private NoviceTrack() : base(
            title: $"Трасса для {Novices.Instance.TitleParrental}",
            cost: new UnitValue(30_000, Credits.Instance),
            upkeep: new UnitValue(10_000, Credits.Instance),
            serviceType: NeedType.Track,
            servicesData: new UnitValue(20, Novices.Instance))
        { }
    }
}
