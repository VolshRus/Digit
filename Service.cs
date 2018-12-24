using Resort.Types.Needs;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort
{
    internal class Service    {        public readonly Visitor _visitorType;        public NeedType NeedType;        private int _servicedNow;        public int ServicedMax { get; private set; }        public Service(NeedType needType, int servicedMax, Visitor visitorType)        {            NeedType = needType;            ServicedMax = servicedMax;            _visitorType = visitorType;        }        public override string ToString()        {            return ServicedMax + " " + _visitorType.ShortTitleParrental;        }    }
}
