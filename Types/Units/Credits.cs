namespace Resort.Types.Units
{
    internal class Credits : Unit    {        public static Credits Instance { get; } = new Credits();        private Credits() : base(            title: "Кредиты",            shortTitle: "Cr",            titleParrental: "кредитов",            shortTitleParrental: "cr.")        { }    }
}
