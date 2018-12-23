using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Types.Units
{
    class Pro : Visitor    {        public static new Pro Instance { get; } = new Pro();        private Pro() : base(            title: "Профессионалы",            shortTitle: "Профи",            titleParrental: "профессионалов",            shortTitleParrental: "профи")        { }    }
}
