using Resort.Types.Needs;
using Resort.Types.Units;
using System.Linq;

namespace Resort.Types.Buildings
{
    internal abstract class BuildingType
    {
        public string Title { get; private set; }
        public UnitValue Cost { get; private set; }
        public UnitValue Upkeep { get; private set; }
        public NeedType ServiceType { get; private set; }
        public UnitValue[] ServiceVolume { get; private set; }

        public string Description => $"{Title} - {Cost}, обслуживает: {ServiceVolume.Select(t => t.ToString()).Aggregate((t1, t2) => $"{t1} и {t2}")}";

        protected BuildingType(string title, UnitValue cost, UnitValue upkeep, NeedType serviceType, params UnitValue[] servicesData)
        {
            Title = title;
            Cost = cost;
            Upkeep = upkeep;
            ServiceType = serviceType;
            ServiceVolume = servicesData;
        }
    }
}
