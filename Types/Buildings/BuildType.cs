using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;
using System.Linq;

namespace Resort.Types.Buildings
{
    internal abstract class BuildType
    {
        public string Title { get; private set; }
        public Money Cost { get; private set; }
        public Money Upkeep { get; private set; }
        public NeedType NeedType { get; private set; }
        public ClientsAmount[] ClientsData { get; private set; }

        public string Description => $"{Title} - {Cost}, обслуживает: {ClientsData.Select(t => t.ToString()).Aggregate((t1, t2) => $"{t1} и {t2}")}";

        protected BuildType(string title, Money cost, Money upkeep, NeedType serviceType, params ClientsAmount[] clientsData)
        {
            Title = title;
            Cost = cost;
            Upkeep = upkeep;
            NeedType = serviceType;
            ClientsData = clientsData;
        }
    }
}
