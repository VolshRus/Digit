using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Types.Units
{
    class Novices : Visitor    {        public static new Novices Instance { get; } = new Novices();        private Novices() : base(            title: "Начинающие",            shortTitle: "Новички",            titleParrental: "начинающих",            shortTitleParrental: "новичков")        { }    }
}
