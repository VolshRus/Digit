using System;
using System.Collections.Generic;
using System.Linq;
using Resort.Types;
using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort
{
    class ClientManager
    {
        public ClientManager()
        {
            InitializeClients();
        }
        public Chance RatingDecreaseByLeavers() => _clientsLeavedAngry.Select(t => t.RatingDecrease).Sum();
        public int ClientsAmount => _clients.Count();
        public Money Rent => _clients.Select(t => t.Rent).Sum();
        public int ClientsHappyLeaved<T>() where T : ClientType => _clientsLeaveHappy.Count(t => t.ClientType.GetType() == typeof(T));
        public int ClientsAngryLeaved<T>() where T : ClientType => _clientsLeavedAngry.Count(t => t.ClientType.GetType() == typeof(T));
        public IEnumerable<Client> ClientsByType(ClientType clientType) => _clients.Where(t => t.ClientType == clientType);

        public void ClientsArrive<T>(int amount) where T : ClientType
        {
            var newClients = new List<Client>();
            var type = (T)typeof(T).GetProperty("Instance").GetValue(null);

            for (int i = 0; i < amount; i++)
            {
                newClients.Add(new Client(type));
            }
            _clients.AddRange(newClients);
            _clientsArrived.AddRange(newClients);
        }

        public void ClientsHappyLeave()
        {
            var leavingClients = _clients.Where(t => t.Lasting.Value == 0).ToList();
            leavingClients.ForEach(t => _clients.Remove(t));
            _clientsLeaveHappy.AddRange(leavingClients);
        }

        public void ClientsAngryLeave(int amount, ClientType clientType = null)
        {
            if (amount > 0)
            {
                var leavingClients =
                    clientType == null
                    ? _clients.TakeLast(amount)
                    : ClientsByType(clientType).TakeLast(amount);
                leavingClients.ForEach(t => _clients.Remove(t));
                _clientsLeavedAngry.AddRange(leavingClients);
            }
        }
        public void NextTurn()
        {
            _clientsLeaveHappy.Clear();
            _clientsLeavedAngry.Clear();
            _clientsArrived.Clear();
            foreach (var client in _clients)
            {
                client.Lasting = new Duration(client.Lasting.Value - 1);
            }
        }
        private void InitializeClients()
        {
            for (int i = 0; i < _novicesOnStart; i++)
            {
                _clients.Add(new Client(NoviceClientType.Instance));
            }
            for (int i = 0; i < _proOnStart; i++)
            {
                _clients.Add(new Client(ProClientType.Instance));
            }
        }

        List<Client> _clients = new List<Client>();
        List<Client> _clientsLeaveHappy = new List<Client>();
        List<Client> _clientsLeavedAngry = new List<Client>();
        List<Client> _clientsArrived = new List<Client>();

        readonly int _novicesOnStart = 20;
        readonly int _proOnStart = 4;

    }
}
