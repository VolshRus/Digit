using System;
using System.Collections.Generic;
using System.Linq;
using Resort.Types;
using Resort.Types.Clients;

namespace Resort
{
    static class Extensions
    {
        public static Money Sum(this IEnumerable<Money> source)
        {
            return source.Aggregate(new Money(0), (x, y) => x + y);
        }

        public static Chance Sum(this IEnumerable<Chance> source)
        {
            return source.Aggregate(new Chance(0), (x, y) => x + y);
        }

        public static ClientsAmount Sum(this IEnumerable<ClientsAmount> source)
        {
            return source.Aggregate((x, y) => x + y);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action.Invoke(item);
            }
            return list;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            var randomised = list.Select(item => new { item, order = rnd.Next() })
                .OrderBy(x => x.order)
                .Select(x => x.item)
                .ToList();

            return randomised;
        }

        static Random rnd = new Random();
    }
}
