using System;
using System.Linq;
using Resort.Types;
using Resort.Types.Buildings;
using Resort.Types.Clients;

namespace Resort
{
    class Game
    {
        public int ArrivingNovices => Rating.Value / 5;
        public int ArrivingPro => Rating.Value / 10;
        public bool IsWin => Money.Value >= 1000000;
        public bool IsLoose => Rating.Value == 0 || Money.Value == 0 || Turn > 20;
        public void NewTurn()
        {
            ClientManager.NextTurn();
            BuildManager.NextTurn();
            AngryCheckOut();
            Money += ClientManager.Rent;
            ClientManager.ClientsHappyLeave();
            ClientManager.ClientsArrive<NoviceClientType>(ArrivingNovices);
            ClientManager.ClientsArrive<ProClientType>(ArrivingPro);
            Rating -= RatingDecreasePerTurn;
            Rating += PromoRating;
            Turn++;
        }

        public bool TryBuild(BuildType buildType)
        {
            if (BuildManager.CanBuild > 0 && buildType.Cost <= Money)
            {
                Money -= buildType.Cost;
                BuildingsCost += buildType.Cost;
                BuildManager.Build(buildType);
                return true;
            }
            return false;
        }

        private void AngryCheckOut()
        {
            ClientType novice = NoviceClientType.Instance;
            ClientType pro = ProClientType.Instance;

            int novicesAmount = ClientManager.ClientsByType(novice).Count();
            int proAmount = ClientManager.ClientsByType(pro).Count();
            int clientsAmount = ClientManager.ClientsAmount;
            int servicedNovices = BuildManager.MinimalService(novice);
            int servicedPro = BuildManager.MinimalService(pro);
            int servicedClients = BuildManager.MinimalService();

            ClientManager.ClientsAngryLeave(novicesAmount - servicedNovices, novice);
            ClientManager.ClientsAngryLeave(proAmount - servicedPro, pro);
            ClientManager.ClientsAngryLeave(clientsAmount - servicedClients);

            Rating -= ClientManager.RatingDecreaseByLeavers();

        }

        public DateTime Today => new DateTime(3030, 01, 01).AddDays(Turn);
        public BuildManager BuildManager = new BuildManager();
        public ClientManager ClientManager = new ClientManager();

        public Chance Rating { get; private set; } = new Chance(44);
        public Chance RatingDecreasePerTurn { get; private set; } = new Chance(10);

        public Money BuildingsCost = new Money(0);
        public Money PromoCost => PromoStepCost * PromoMultiplier;
        public Chance PromoRating => _promoIncreasePerMultiplier * PromoMultiplier;
        public int LeavingClientsByType(ClientType clientType) => Math.Max(0, ClientManager.ClientsByType(clientType).Count() - BuildManager.MinimalService(clientType));
        public Money Money { get; private set; } = new Money(100_000);


        public int PromoMultiplier;
        private int Turn;
        private Chance _promoIncreasePerMultiplier = new Chance(5);
        public readonly Money PromoStepCost = new Money(10_000);

    }


}
