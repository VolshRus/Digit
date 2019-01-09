using System;
using Resort.Types.Units;

namespace Resort.Types.Clients
{
    class NoviceClientType : ClientType
    {
        public static NoviceClientType Instance { get; } = new NoviceClientType();
        public override Money Rent => new Money(2000);
        public override Chance RatingDecrease => new Chance(1);
        public override Range Lasting => new Range(1, 6);
        public override Unit Unit => Novice.Instance;
        public override ClientsAmount CreateClients(int value)
        {
            return new NovicesAmount(value);
        }
        private NoviceClientType() { }
    }
}
