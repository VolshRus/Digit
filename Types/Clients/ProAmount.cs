using System;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Clients
{
    class ProAmount : ClientsAmount
    {
        public ProAmount(int value) : base(value, Pro.Instance)
        { }

        public override Service CreateService(NeedType needTypes)
        {
            return new SpecialService(needTypes, Value, ProClientType.Instance);
        }

        public override ClientsAmount Add(ClientsAmount another)
        {
            return new ProAmount(Value + another.Value);
        }

        public override ClientsAmount Add(int another)
        {
            return new NovicesAmount(Value + another);
        }
    }
}
