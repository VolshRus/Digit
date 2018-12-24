using Resort.Types.Buildings;
using Resort.Types.Units;
using System.Collections.Generic;

namespace Resort.Buildings
{
    internal class Building
    {
        public string Title => _type.Title;
        public UnitValue Cost => _type.Cost;
        public UnitValue Upkeep => _type.Upkeep;
        public IReadOnlyCollection<Service> Services => _services;

        public Building(BuildingType type)
        {
            _type = type;
            _services = new List<Service>(_type.ServiceVolume.Length);
            foreach (var data in _type.ServiceVolume)
            {
                _services.Add(new Service(type.ServiceType, data.Value, data.Unit as Visitor));
            }
        }

        private BuildingType _type;
        private List<Service> _services;
    }
}
