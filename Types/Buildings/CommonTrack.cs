using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Buildings
{
    internal class CommonTrack : BuildType
    {
        public static CommonTrack Instance { get; } = new CommonTrack();

        private CommonTrack() : base(
            "Обычная трасса",
             new Money(45_000),
             new Money(15_000),
             NeedType.Track,
             new NovicesAmount(10),
             new ProAmount(10)
        )
        { }

    }
}
