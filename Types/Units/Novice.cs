using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Types.Units
{
    class Novice : Visitor    {        public static new Novice Instance { get; } = new Novice();        private Novice() : base(            title: "Начинающие",            shortTitle: "Новички",            titleParrental: "начинающих",            shortTitleParrental: "новичков")        { }    }
}
