using Resort.Types;
using Resort.Types.Units;
using System.Collections.Generic;
using System.Linq;

namespace Resort.Buildings
{
    internal abstract class Building
    {
        public abstract string Title { get; }
        public abstract Money Cost { get; }
        public List<Service> Services { get; }
        protected Building((int, Visitor)[] servicesData)
        {
            Services = new List<Service>(servicesData.Length);
            foreach (var data in servicesData)
            {
                Services.Add(new Service(data.Item1, data.Item2));
            }
        }
        public override string ToString()
        {
            return $"{Title} - {Cost}, обслуживает: {Services.Select(t => t.ToString()).Aggregate((t1, t2) => $"{t1} и {t2}")}";
        }

        public static implicit operator string(Building input)        {
            return $"{input.Title} - {input.Cost}, обслуживает: {input.Services.Select(t => t.ToString()).Aggregate((t1, t2) => $"{t1} и {t2}")}";
        }
    }
}
