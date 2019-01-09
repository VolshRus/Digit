using System;
using System.Collections.Generic;
using System.Linq;
using Resort.Buildings;
using Resort.Types;
using Resort.Types.Buildings;
using Resort.Types.Clients;
using Resort.Types.Needs;

namespace Resort
{
    class BuildManager
    {
        public IReadOnlyCollection<BuildType> AvailableBuildings => _availableBuildings;
        public int CanBuild => _buildPerTurn - _buildsCount;
        public Money Upkeep => _buildings.Select(t => t.Upkeep).Sum();

        public BuildManager()
        {
            Initialize();
        }

        public int MinimalService()
        {
            List<NeedType> except = new List<NeedType>();
            except.Add(NeedType.Track);
            return Enum.GetValues(typeof(NeedType))
                .Cast<NeedType>()
                .Except(except)
                .Select(t => ServicedClients(t).Value)
                .Min();
        }

        public int MinimalService(ClientType clientType)
        {
            return Enum.GetValues(typeof(NeedType))
                .Cast<NeedType>()
                .Select(t => ServicedClients(clientType, t).Value + ServicedClients(t).Value)
                .Min();
        }

        private ClientsAmount ServicedClients(NeedType needType)
        {
            return _buildings
                .SelectMany(t => t.Services)
                .Where(t => t is CommonService)
                .Where(t => t.NeedType == needType)
                .Select(t => new ClientsAmount(t.ServicedMax))
                .Append(new ClientsAmount(0))
                .Sum();
        }
        private ClientsAmount ServicedClients(ClientType clientType, NeedType needType)
        {
            return _buildings
                .SelectMany(t => t.Services)
                .Where(t => t is SpecialService)
                .Cast<SpecialService>()
                .Where(t => t.NeedType == needType)
                .Where(t => t.ClientType == clientType)
                .Select(t => clientType.CreateClients(t.ServicedMax))
                .Append(clientType.CreateClients(0))
                .Sum();
        }

        public IEnumerable<UnitValue> ServicesByBuildType(BuildType buildingType)
        {
            return buildingType.ClientsData.Select(t => t * _buildings.Count(b => b.Title == buildingType.Title));
        }

        public void Build(BuildType type)
        {
            _buildings.Add(new Building(type));
            _buildsCount++;
        }

        public void NextTurn()
        {
            _buildsCount = 0;
        }

        public void Initialize()
        {
            _availableBuildings.Add(Hotel.Instance);
            _availableBuildings.Add(Restaurant.Instance);
            _availableBuildings.Add(NoviceTrack.Instance);
            _availableBuildings.Add(CommonTrack.Instance);
            _availableBuildings.Add(ProTrack.Instance);
            _availableBuildings.Add(Chairlift.Instance);
            _availableBuildings.Add(Cabinlift.Instance);

            _buildings.Add(new Building(Hotel.Instance));
            _buildings.Add(new Building(CommonTrack.Instance));
            _buildings.Add(new Building(Restaurant.Instance));
            _buildings.Add(new Building(Chairlift.Instance));

        }

        int _buildsCount = 0;
        int _buildPerTurn = 2;
        List<Building> _buildings = new List<Building>();
        List<BuildType> _availableBuildings = new List<BuildType>();
    }
}
