using NullGuard;using Resort.Buildings;
using Resort.Types;
using Resort.Types.Buildings;
using Resort.Types.Clients;
using Resort.Types.Needs;
using Resort.Types.Units;
using System;using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;using System.Reflection;
using System.Runtime.InteropServices;

[assembly: NullGuard(ValidationFlags.All)]namespace Resort{    class Game    {        public void AngryCheckOut()
        {            ClientType novice = NoviceClientType.Instance;            ClientType pro = ProClientType.Instance;            int novicesAmount = ClientManager.ClientsByType(novice).Count();            int proAmount = ClientManager.ClientsByType(pro).Count();            int clientsAmount = ClientManager.ClientsAmount;            int servicedNovices = BuildManager.MinimalService(novice);
            int servicedPro = BuildManager.MinimalService(pro);            int servicedClients = BuildManager.MinimalService();
            ClientManager.ClientsLeave(novicesAmount - servicedNovices, novice, true);            ClientManager.ClientsLeave(proAmount - servicedPro, pro, true);
            ClientManager.ClientsLeave(clientsAmount - servicedClients, true);            Rating -= ClientManager.RatingDecreaseByLeavers();
        }        public DateTime Today => new DateTime(3029, 12, 31).AddDays(Turn);        public BuildManager BuildManager = new BuildManager();        public ClientManager ClientManager = new ClientManager();        public Chance Rating { get; private set; } = new Chance(44);        public Chance RatingDecreasePerTurn { get; private set; } = new Chance(10);        public Money PromoCost => _promoStepCost * _promoMultiplier;        public Chance PromoRating => _promoIncreasePerMultiplier * _promoMultiplier;        public int LeavingClientsByType(ClientType clientType) => Math.Max(0, ClientManager.ClientsByType(clientType).Count() - BuildManager.MinimalService(clientType));        public Money Money { get; private set; } = new Money(100_000);        private int _promoMultiplier;        private int Turn;        private Chance _promoIncreasePerMultiplier = new Chance(5);        private Money _promoStepCost = new Money(10_000);    }    internal interface IAction
    {
        IAction Do();
    }

    internal class Action : IAction
    {
        public IAction Do() { return new Action(); }
    }

    class ShowAction : IAction
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
    class BuildAction : IAction
    {
        public IAction Do() { return new BuildAction(); }
    }
    internal interface IView
    {
        IAction GetAction();
    }
    class BuildView : IView
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
        List<BuildType> _availableBuildings = new List<BuildType>();
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
    internal class MenuView : IView
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

    internal class TurnStartView : IView
    {
        public TurnStartView(Game g)
        {
            _game = g;
        }
        public void ShowTable(string[,] rows)
        {
            int[] MaxWidth = new int[rows.GetUpperBound(1) + 1];
            for (int i = 0; i < MaxWidth.Length; i++)
            {
                int Max = rows[0, i].Length;
                for (int j = 1; j < rows.GetUpperBound(0); j++)
                {
                    if (rows[j, i].Length > Max) Max = rows[j, i].Length;
                }
                MaxWidth[i] = Max;
            }
            for (int i = 0; i <= rows.GetUpperBound(0); i++)
            {
                for (int j = 0; j < MaxWidth.Length; j++)
                {
                    if (j == 0) Console.Write(rows[i, j].PadRight(MaxWidth[j] + 10));
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
                {"Туристы:", _game.ClientManager.Rent, ""},
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
                {"Недовольные новички:",_game.LeavingClientsByType(NoviceClientType.Instance).ToString(),""},
                {"Недовольные профи",_game.LeavingClientsByType(ProClientType.Instance).ToString(),"" },
                {"Естественное снижение",_game.RatingDecreasePerTurn,"" },
                {"Итого:","","" }
            };

            ShowTable(promoTable);
            Console.WriteLine();

            string[,] clientsTable =
            {
                {"Посетители, отчет", "Уехало довольных", "Уехало недовольных", "Приехало", "Итого"},
                {"Новички:","", "","",""},
                {"Профи:", "", "","",""}
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

    internal class Program    {        [DllImport("libc")]        private static extern int system(string exec);

        private static void Main(string[] args)        {
            system(@"printf '\e[8;26;150t'");
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
            TurnStartView view = new TurnStartView(g);
            IAction action = view.GetAction();            while (true)
            {
                action = action.Do();
            }        }    }}