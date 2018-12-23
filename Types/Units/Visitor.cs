namespace Resort.Types.Units
{
    internal class Visitor : Unit    {        public static Visitor Instance { get; } = new Visitor();        protected Visitor(string title, string shortTitle, string titleParrental, string shortTitleParrental)            : base(title, shortTitle, titleParrental, shortTitleParrental)
        { }        private Visitor() : base(            title: "Клиенты",            shortTitle: "Гости",            titleParrental: "клиентов",            shortTitleParrental: "гостей")        { }    }
}
