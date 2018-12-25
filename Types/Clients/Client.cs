using System;
using System.Collections.Generic;
using System.Linq;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Clients
{
    class Client
    {
        public ClientType ClientType { get; }
        public Money Rent { get; }
        public Chance RatingDecrease { get; }
        public Duration Lasting { get; private set; }
        public Unit Unit { get; }
        public bool Satisfied => _needs.All(t => t.Satisfied);
        public Client(ClientType type)
        {
            ClientType = type;
            Rent = type.Rent;
            RatingDecrease = type.RatingDecrease;
            Unit = type.Unit;
            var (start, end) = type.Lasting;
            Lasting = new Duration(random.Next(start, end + 1));
            //TODO:change
            _needs.Add(new FoodNeed());
            _needs.Add(new RoomNeed());
            _needs.Add(new TrackNeed());
            _needs.Add(new ElevatorNeed());
        }

        protected List<Need> _needs = new List<Need>();
        protected static readonly Random random = new Random();
    }
}
