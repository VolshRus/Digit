using NullGuard;
using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort
{
    internal class Service    {        public NeedType NeedType;
        public int ServicedMax { get; private set; }
        public Service(NeedType needType, int servicedMax)
        {
            NeedType = needType;
            ServicedMax = servicedMax;
        }

        public override string ToString()
        {
            return ServicedMax + " " + Visitor.Instance.ShortTitleParrental;
        }    }
    class SpecialService : Service
    {
        public readonly ClientType ClientType;
        public SpecialService(NeedType needType, int servicedMax, ClientType clientType)
            : base(needType, servicedMax)
        {
            ClientType = clientType;
        }

        public override string ToString()
        {
            return ServicedMax + " " + ClientType.Unit.ShortTitleParrental;
        }
    }
}
