using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Types.Units
{
    class Credits : Unit    {        public static Credits Instance { get; } = new Credits();        private Credits() : base(            title: "Кредиты",            shortTitle: "Cr",            titleParrental: "кредитов",            shortTitleParrental: "cr.")        { }    }
}
