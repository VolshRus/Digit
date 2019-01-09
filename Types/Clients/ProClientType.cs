using System;
using Resort.Types.Units;

namespace Resort.Types.Clients
{
    class ProClientType : ClientType
    {
        public static ProClientType Instance { get; } = new ProClientType();

        public override Money Rent => new Money(6000);
        public override Chance RatingDecrease => new Chance(2);
        public override Range Lasting => new Range(1, 10);
        public override Unit Unit => Pro.Instance;
        public override ClientsAmount CreateClients(int value)
        {
            return new ProAmount(value);
        }
        private ProClientType() { }
    }
}
