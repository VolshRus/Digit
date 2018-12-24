using NullGuard;using Resort.Buildings;
using Resort.Types.Buildings;
using Resort.Types.Needs;
using Resort.Types.Units;
using System;using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
[assembly: NullGuard(ValidationFlags.All)]namespace Resort{    static class Extensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach(var item in list)
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
    }    class BuildManager
    {
        public IReadOnlyCollection<BuildingType> AvailableBuildings => _availableBuildings;
        public bool CanBuild => _buildsCount < _buildPerTurn;
        public UnitValue Upkeep => _buildings.Select(t => t.Upkeep).Aggregate((t1, t2) => t1 + t2);
        public BuildManager()
        {
            Initialize();
        }
        public int GetBuildingsAmount(BuildingType buildingType) => _buildings.Count(b => b.Title == buildingType.Title);
        public int MinimalService(Visitor visitor)
        {
            var a = Enum.GetValues(typeof(NeedType))
                .Cast<NeedType>()
                .Select(t => GetServiceAmount(t, visitor));
             return a.Min();
        }
        public int GetServiceAmount(NeedType needType, Visitor visitorType)
        {
            return _buildings
                .SelectMany(t => t.Services)
                .Where(t => t.NeedType == needType && (visitorType == t._visitorType || visitorType.GetType().IsSubclassOf(t._visitorType.GetType())))
                .Select(t => t.ServicedMax)
                .Sum();
        }
        public string GetServiceDescription(BuildingType buildingType)
        {
            return buildingType.ServiceVolume
                .Select(t => new UnitValue(t.Value * GetBuildingsAmount(buildingType), t.Unit).ToString())
                .Aggregate((t1, t2) => t1 + " и " + t2);
        }
        public void Build(BuildingType type)
        {
            _buildings.Add(new Building(type));
            _buildsCount++;
        }
        public IReadOnlyCollection<Service> GetServices() => _buildings.SelectMany(t => t.Services) as IReadOnlyCollection<Service>;
        public void Initialize()
        {
            _availableBuildings.Add(Hotel.Instance);
            _availableBuildings.Add(Restaurant.Instance);
            _availableBuildings.Add(NoviceTrack.Instance);
            _availableBuildings.Add(CommonTrack.Instance);
            _availableBuildings.Add(ProTrack.Instance);
            _availableBuildings.Add(Chairlift.Instance);
            _availableBuildings.Add(Cabinlift.Instance);

            _buildings.Add(new Building(Hotel.Instance));
            _buildings.Add(new Building(Hotel.Instance));
            _buildings.Add(new Building(CommonTrack.Instance));
            _buildings.Add(new Building(CommonTrack.Instance));
            _buildings.Add(new Building(Restaurant.Instance));
            _buildings.Add(new Building(NoviceTrack.Instance));
            _buildings.Add(new Building(Chairlift.Instance));

        }
        int _buildsCount = 0;
        int _buildPerTurn = 2;
        List<Building> _buildings = new List<Building>();
        List<BuildingType> _availableBuildings = new List<BuildingType>();
    }    class ClientManager
    {
        public ClientManager()
        {
            InitializeClients();
        }

        public UnitValue GetRent() => _clients.Select(t => t.Rent).Aggregate((t1, t2) => t1 + t2);
        public IEnumerable<Client> ClientsByType(Visitor visitor) => _clients.Where(t => t.Unit == visitor);

        public void ClientsArrive(int amount, Visitor type)
        {
            for (int i = 0; i < amount; i++)
            {
                _clients.Add(new NoviceClient());
            }
        }

        public void ClientsLeave(int amount, Visitor type)
        {
            var leavingClients = _clients.Where(t => t.Unit == type).TakeLast(amount);
            _clientsLeaved.AddRange(leavingClients);
            leavingClients.ForEach(t => _clients.Remove(t));
        }

        private void InitializeClients()
        {
            for (int i=0;i< _novicesOnStart; i++)
            {
                _clients.Add(new NoviceClient());
            }
            for (int i=0;i< _proOnStart; i++)
            {
                _clients.Add(new ProClient());
            }
        }

        List<Client> _clients = new List<Client>();
        List<Client> _clientsLeaved = new List<Client>();
        List<Client> _clientsArrived = new List<Client>();

        int _novicesOnStart = 20;
        int _proOnStart = 0;

    }    abstract class Client
    {
        public abstract UnitValue Rent { get; }
        public abstract UnitValue RatingDecrease { get; }
        public abstract int StayDaysLeft { get; }
        public abstract Unit Unit { get; }
        public bool Satisfied => _needs.All(t => t.Satisfied);
        protected Client()
        {
            _needs.Add(new FoodNeed());
            _needs.Add(new RoomNeed());
            _needs.Add(new TrackNeed());
            _needs.Add(new ElevatorNeed());
        }
        public UnitValue PayRent()
        {
            _needs.ForEach(t => t.Refresh());
            return Rent;
        }
        protected List<Need> _needs = new List<Need>();
        protected static readonly Random random = new Random();
    }    class ProClient:Client
    {
        public override UnitValue Rent => new UnitValue(6000, Credits.Instance);
        public override UnitValue RatingDecrease => new UnitValue(2, Percents.Instance);
        public override int StayDaysLeft => random.Next(1, 10);
        public override Unit Unit => Pro.Instance;

    }    class NoviceClient:Client
    {
        public override UnitValue Rent => new UnitValue(2000, Credits.Instance);
        public override UnitValue RatingDecrease => new UnitValue(1, Percents.Instance);
        public override int StayDaysLeft => random.Next(1, 6);
        public override Unit Unit => Novices.Instance;
    }    internal interface IAction
    {
        IAction Do();
    }

    internal class Action : IAction
    {
       public IAction Do() { return new Action(); }
    }

    class ShowAction:IAction
    {
        public ShowAction(IView view)
        {
            _shownView = view;
        }

        public IAction Do()
        {
            return _shownView.GetAction();
        }

        IView _shownView;
    }
    class BuildAction:IAction
    {
        public IAction Do() { return new BuildAction(); }
    }
    internal interface IView
    {
        IAction GetAction();
    }
    class BuildView:IView
    {
        public BuildView()
        {
            _availableBuildings.Add(Hotel.Instance);
            _availableBuildings.Add(Restaurant.Instance);
            _availableBuildings.Add(NoviceTrack.Instance);
            _availableBuildings.Add(CommonTrack.Instance);
            _availableBuildings.Add(ProTrack.Instance);
            _availableBuildings.Add(Chairlift.Instance);
            _availableBuildings.Add(Cabinlift.Instance);
        }

        public IAction GetAction()
        {
            var menu = new MenuBuilder();
            _availableBuildings.ForEach(t => menu.AddItem(t.Description, new BuildAction()));
            return menu.Build().GetAction();
        }
        List<BuildingType> _availableBuildings = new List<BuildingType>();
    }
    class MenuBuilder
    {
        public MenuBuilder AddItem(string text, IAction action)
        {
            _list.Add((text, action));
            return this;
        }

        public MenuView Build()
        {
            return new MenuView(_list);
        }
        private List<(string, IAction)> _list = new List<(string, IAction)>();
    }
    internal class MenuView:IView
    {
        public MenuView(IReadOnlyCollection<(string, IAction)> items)
        {
            Contract.Requires(items.Count > 0);

            _list.AddRange(items);
            _selectedItem = _list.First();
        }

        public IAction GetAction()
        {
            _menuStartX = Console.CursorTop;
            Console.CursorVisible = false;
            foreach (var item in _list)
            {
                string selectedMark = item.Equals(_selectedItem) ? ">" : " ";
                Console.WriteLine($"{selectedMark} {item.Item1}");
            }
            while (true)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                Console.SetCursorPosition(0, _menuStartX + _selectedIndex);
                Console.Write(" ");
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        _selectedItem = _selectedItem.Equals(_list.First()) ? _list.Last() : _list[_selectedIndex - 1];
                        break;
                    case ConsoleKey.DownArrow:
                        _selectedItem = _selectedItem.Equals(_list.Last()) ? _list.First() : _list[_selectedIndex + 1];
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        Console.SetCursorPosition(0, _menuStartX + _list.Count + 1);
                        return _selectedItem.Item2;
                }
                Console.SetCursorPosition(0, _menuStartX + _selectedIndex);
                Console.Write(">");
            }
        }

        private int _menuStartX;

        private int _selectedIndex => _list.IndexOf(_selectedItem);

        private (string, IAction) _selectedItem;
        private List<(string, IAction)> _list = new List<(string, IAction)>();
    }

    internal class TurnStartView:IView
    {
        public TurnStartView(Game g)
        {
            _game = g;
        }
        public void ShowTable(string[,] rows)
        {
            int[] MaxWidth = new int[rows.GetUpperBound(1)+1];
            for (int i=0;i<MaxWidth.Length;i++)
            {
                int Max = rows[0,i].Length;
                for (int j=1;j<rows.GetUpperBound(0);j++)
                {
                    if (rows[j, i].Length > Max) Max = rows[j, i].Length;
                }
                MaxWidth[i] = Max;
            }
            for (int i = 0; i <= rows.GetUpperBound(0); i++)
            {
                for (int j = 0; j < MaxWidth.Length; j++)
                {
                    if (j==0) Console.Write(rows[i,j].PadRight(MaxWidth[j] + 10));
                    else Console.Write(rows[i, j].PadLeft(MaxWidth[j] + 10));

                }
                Console.WriteLine();
            }
        }
        public IAction GetAction()
        {
            Console.WriteLine($"День: {_game.Today}");
            Console.WriteLine();

            string[,] financialTable =
            {
                {"Деньги, прогноз на завтра","ДЕБЕТ", "КРЕДИТ"},
                {"Средств на счету:", _game.Money, ""},
                {"Туристы:", _game.ClientManager.GetRent(), ""},
                {"Содержание зданий:", "",_game.BuildManager.Upkeep},
                {"Реклама:","",_game.PromoCost },
                {"Строительство","","" },
                {"Итого:","","" }
            };

            ShowTable(financialTable);
            Console.WriteLine();

            string[,] promoTable =
            {
                {"Рейтинг, прогноз на завтра","Снижение", "Увеличение"},
                {"Текущий рейтинг:","", _game.Rating},
                {"Реклама:", "", _game.PromoRating},
                {"Недовольные новички:",_game.LeavingClientsByType(Novices.Instance).ToString(),""},
                {"Недовольные профи",_game.LeavingClientsByType(Pro.Instance).ToString(),"" },
                {"Естественное снижение",_game.RatingDecreasePerTurn,"" },
                {"Итого:","","" }
            };

            ShowTable(promoTable);
            Console.WriteLine();

            string[,] clientsTable =
            {
                {"Посетители, отчет", "Уехало довольных", "Уехало недовольных", "Приехало", "Итого"},
                {"Новички:","", "","",""},
                {"Профи:", "", "","",""},
            };

            ShowTable(clientsTable);
            Console.WriteLine();
            
            //Console.WriteLine();
            //Console.WriteLine($"Уехало довольных     Уехало недовольных         Приехало");
            //Console.WriteLine($"Новичков: сейчас 30 (уехало 10, приехало 8)");
            //Console.WriteLine($"Профессионалов: сейчас 30 (уехало 10, приехало 8)");

            var menu = new MenuBuilder();
            return menu.AddItem("Построить что-нибудь", new ShowAction(new BuildView()))
                .AddItem("Изменить рекламу", new Action())
                .AddItem("Ждать конца дня", new Action())
                .Build().GetAction();
        }
        Game _game;
    }
    class Game
    {
        public DateTime Today => new DateTime(3029, 12, 31).AddDays(Turn);
        public BuildManager BuildManager = new BuildManager();
        public ClientManager ClientManager = new ClientManager();

        public UnitValue Rating { get; private set; } = new UnitValue(44, Percents.Instance);
        public UnitValue RatingDecreasePerTurn { get; private set; } = new UnitValue(10, Percents.Instance);

        public UnitValue PromoCost => _promoStepCost * _promoMultiplier;
        public UnitValue PromoRating => _promoIncreasePerMultiplier * _promoMultiplier;
        public int LeavingClientsByType(Visitor type) => Math.Max(0, ClientManager.ClientsByType(type).Count() - BuildManager.MinimalService(type));
        public UnitValue Money { get; private set; } = new UnitValue(100_000, Credits.Instance);
        

        private int _promoMultiplier = 0;
        private int Turn = 0;
        private UnitValue _promoIncreasePerMultiplier = new UnitValue(5, Percents.Instance);
        private UnitValue _promoStepCost = new UnitValue(10_000, Credits.Instance);

    }
    internal class Program    {        private static void Main(string[] args)        {

            //         var subclassTypes = Assembly
            //.GetAssembly(typeof(BuildingType))
            //.GetTypes()
            //.Where(t => t.IsSubclassOf(typeof(BuildingType)));
            //         foreach (var building in subclassTypes)
            //         {
            //             var x = ((BuildingType)building.GetProperty("Instance").GetValue(null)).Description;
            //             Console.WriteLine(x);
            //Console.WriteLine(((BuildingType)Activator.CreateInstance(building)).Description);
            //}
            BuildManager bm = new BuildManager();
            Game g = new Game();
            g.ClientManager.ClientsLeave(10, Pro.Instance);
            TurnStartView view = new TurnStartView(g);
            IAction action = view.GetAction();            while (true)
            {
                action = action.Do();
            }        }    }}