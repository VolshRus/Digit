using System;
using System.Collections.Generic;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Clients
{
    abstract class ClientType
    {
        public abstract Money Rent { get; }
        public abstract Chance RatingDecrease { get; }
        public abstract Range Lasting { get; }
        public abstract Unit Unit { get; }
        public abstract ClientsAmount CreateClients(int value);
        public List<NeedType> Needs = new List<NeedType>
        {
            NeedType.Food,
            NeedType.Room,
            NeedType.Track,
            NeedType.Elevator
        };
    }
}
