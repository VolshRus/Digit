using System;
using Resort.Types.Needs;
using Resort.Types.Units;

namespace Resort.Types.Clients
{
    class NovicesAmount : ClientsAmount
    {
        public NovicesAmount(int value) : base(value, Novice.Instance)
        { }

        public override Service CreateService(NeedType needTypes)
        {
            return new SpecialService(needTypes, Value, NoviceClientType.Instance);
        }

        public override ClientsAmount Add(ClientsAmount another)
        {
            return new NovicesAmount(Value + another.Value);
        }

        public override ClientsAmount Add(int another)
        {
            return new NovicesAmount(Value + another);
        }
    }
}
