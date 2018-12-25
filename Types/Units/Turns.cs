using System;
namespace Resort.Types.Units
{
    internal class Turns : Unit
    {
        public static Turns Instance { get; } = new Turns();

        private Turns() : base(
            title: "Дни",
            shortTitle: "Дн.",
            titleParrental: "дней",
            shortTitleParrental: "дн.")
        { }
    }
}
