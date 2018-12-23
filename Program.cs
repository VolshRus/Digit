using System;
using NullGuard;

[assembly: NullGuard(ValidationFlags.All)]
namespace Resort
{
    abstract class Unit
    {
        public string Title;
        public string ShortTitle;
        public string TitleParrental;
        public string ShortTitleParrental;

        protected Unit(string title, string shortTitle, string titleParrental, string shortTitleParrental)
        {
            Title = title;
            ShortTitle = shortTitle;
            TitleParrental = titleParrental;
            ShortTitleParrental = shortTitleParrental;
        }
    }

    abstract class UnitValue
    {
        public int Value { get; private set; }
        public Unit Unit { get; private set; }

        protected UnitValue(int value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }
        public static implicit operator string(UnitValue input)
        {
            return $"{input.Value} {input.Unit.ShortTitleParrental}";
        }
    }

    class Money : UnitValue
    {
        public Money(int value) : base(value, Credit.Instance)
        { }

        public static UnitValue operator +(Money left, Money right)
        {
            return new Money(left.Value + right.Value);
        }
    }

    class Credit : Unit
    {
        public static Credit Instance { get; } = new Credit();

        private Credit() : base(
            title: "Кредиты",
            shortTitle: "Cr",
            titleParrental: "кредитов",
            shortTitleParrental: "cr.")
        { }
    }

    class Novice : Unit
    {
        public static Novice Instance { get; } = new Novice();

        private Novice() : base(
            title: "Начинающие",
            shortTitle: "Новички",
            titleParrental: "начинающих",
            shortTitleParrental: "новичков")
        { }
    }

    class Pro : Unit
    {
        public static Pro Instance { get; } = new Pro();

        private Pro() : base(
            title: "Профессионалы",
            shortTitle: "Профи",
            titleParrental: "профессионалов",
            shortTitleParrental: "профи")
        { }
    }

    class Guest : Unit
    {
        public static Guest Instance { get; } = new Guest();

        private Guest() : base(
            title: "Клиенты",
            shortTitle: "Гости",
            titleParrental: "клиентов",
            shortTitleParrental: "гостей")
        { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Money m = new Money(20);
            Console.Write(m);
            Console.WriteLine("Hello World!");
        }
    }
}
