using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Types.Needs
{
    abstract class Need
    {
        public Need(NeedType needType)
        {
            NeedType = needType;
        }

        public void Satisfy() => Satisfied = true;
        public void Refresh() => Satisfied = false;

        public NeedType NeedType { get; private set; }
        public bool Satisfied { get; private set; }
    } 
}
