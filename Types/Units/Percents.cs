using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Types.Units
{
    internal class Percents : Unit    {        public static Percents Instance { get; } = new Percents();        private Percents() : base(            title: "Проценты",            shortTitle: "%",            titleParrental: "процентов",            shortTitleParrental: "%")        { }    }
}
