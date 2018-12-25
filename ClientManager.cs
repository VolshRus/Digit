using System;
using System.Collections.Generic;
using System.Linq;
using Resort.Types;
using Resort.Types.Clients;
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

        public IEnumerable<Client> ClientsByType(ClientType clientType) => _clients.Where(t => t.ClientType == clientType);

        public void ClientsArrive(int amount, Visitor type)
        {
            var newClients = new List<Client>();

            for (int i = 0; i < amount; i++)
            {
                if (type is Novice)
                {
                    newClients.Add(new Client(NoviceClientType.Instance));
                }
                if (type is Pro)
                {
                    newClients.Add(new Client(ProClientType.Instance));
                }
            }
            _clients.AddRange(newClients);
            _clientsArrived.AddRange(newClients);
        }
        public void ClientsLeave(int amount, bool isAngry)
        {
            if (amount > 0)
            {
                var leavingClients = _clients.TakeLast(amount);
                leavingClients.ForEach(t => _clients.Remove(t));
                if (isAngry)
                {
                    _clientsLeavedAngry.AddRange(leavingClients);
                }
                else
                {
                    _clientsLeaveHappy.AddRange(leavingClients);
                }
            }
        }

        public void ClientsLeave(int amount, ClientType clientType, bool isAngry)
        {
            if (amount > 0)
            {
                var leavingClients = _clients.Where(t => t.ClientType == clientType).TakeLast(amount);
                leavingClients.ForEach(t => _clients.Remove(t));
                if (isAngry)
                {
                    _clientsLeavedAngry.AddRange(leavingClients);
                }
                else
                {
                    _clientsLeaveHappy.AddRange(leavingClients);
                }

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

        int _novicesOnStart = 20;
        int _proOnStart = 0;

    }
}
