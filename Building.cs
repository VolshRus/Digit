using Resort.Types;
using Resort.Types.Buildings;
using Resort.Types.Clients;
using Resort.Types.Units;
using System.Collections.Generic;

namespace Resort.Buildings
{
    internal class Building
    {
        public string Title => _type.Title;
        public Money Cost => _type.Cost;
        public Money Upkeep => _type.Upkeep;
        public IReadOnlyCollection<Service> Services => _services;

        public Building(BuildType type)
        {
            _type = type;
            _services = new List<Service>(_type.ClientsData.Length);
            foreach (ClientsAmount data in _type.ClientsData)
            {
                _services.Add(data.CreateService(type.NeedType));
            }
        }

        private BuildType _type;
        private List<Service> _services;
    }
}
