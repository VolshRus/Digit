using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class CommonTrack : BuildingType
    {
        public static CommonTrack Instance { get; } = new CommonTrack();

        private CommonTrack() : base(
            "Обычная трасса",
             new UnitValue(45_000, Credits.Instance),
             new UnitValue(15_000, Credits.Instance),
             NeedType.Elevator,
             new UnitValue(10, Novices.Instance),
             new UnitValue(10, Pro.Instance)
        )
        { }

    }
}
