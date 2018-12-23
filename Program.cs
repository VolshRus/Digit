using System;using System.Collections.Generic;using NullGuard;using Resort.Types;using Resort.Types.Units;using System.Linq;using Resort.Buildings;using System.Reflection;

[assembly: NullGuard(ValidationFlags.All)]namespace Resort{    class Service    {        readonly Visitor _visitorType;        private int _servicedNow;        public int ServicedMax { get; private set; }        public Service(int servicedMax, Visitor visitorType)        {            ServicedMax = servicedMax;            _visitorType = visitorType;        }        public override string ToString()        {            return ServicedMax + " " + _visitorType.ShortTitleParrental;        }    }            class Program    {        static void Main(string[] args)        {            var subclassTypes = Assembly
   .GetAssembly(typeof(Building))
   .GetTypes()
   .Where(t => t.IsSubclassOf(typeof(Building)));
            foreach(var building in subclassTypes)
            {
                Console.WriteLine(building);
            }
            Console.WriteLine("Hello World!");        }    }}