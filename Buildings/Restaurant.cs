﻿using Resort.Types;
using Resort.Types.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resort.Buildings
{
    class Restaurant:Building
    {
        public override string Title => _title;
        public override Money Cost => _cost;

        public Restaurant() : base(_servicesData)
        { }

        private static string _title = "Ресторан";
        private static Money _cost = new Money(80_000);
        private static (int, Visitor)[] _servicesData =
        {
            (45, Visitor.Instance)
        };
    }
}
