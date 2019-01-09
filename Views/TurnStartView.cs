using System;
using System.Linq;
using Resort.Actions;
using Resort.Types.Clients;

namespace Resort.Views
{
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
            Console.Clear();
            Console.WriteLine($"День: {_game.Today}");
            Console.WriteLine();

            string[,] financialTable =
            {
                {"Деньги, прогноз на завтра","ДЕБЕТ", "КРЕДИТ"},
                {"Средств на счету:", _game.Money+_game.BuildingsCost, ""},
                {"Туристы:", _game.ClientManager.Rent, ""},
                {"Содержание зданий:", "",_game.BuildManager.Upkeep},
                {"Реклама:","",_game.PromoCost },
                {"Строительство","",_game.BuildingsCost },
                {"Итого:","",_game.Money+_game.ClientManager.Rent - _game.BuildManager.Upkeep - _game.PromoCost }
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
                {"Итого:","",_game.Rating+_game.PromoRating - _game.LeavingClientsByType(NoviceClientType.Instance)*NoviceClientType.Instance.RatingDecrease - _game.LeavingClientsByType(ProClientType.Instance) *ProClientType.Instance.RatingDecrease - _game.RatingDecreasePerTurn}
            };

            ShowTable(promoTable);
            Console.WriteLine();

            string[,] clientsTable =
            {
                {"Посетители, отчет", "Уехало довольных", "Уехало недовольных", "Приехало", "Итого"},
                {"Новички:",_game.ClientManager.ClientsHappyLeaved<NoviceClientType>().ToString(), _game.ClientManager.ClientsAngryLeaved<NoviceClientType>().ToString(),_game.ArrivingNovices.ToString(),_game.ClientManager.ClientsByType(NoviceClientType.Instance).Count().ToString()},
                {"Профи:", _game.ClientManager.ClientsHappyLeaved<ProClientType>().ToString(), _game.ClientManager.ClientsAngryLeaved<ProClientType>().ToString(),_game.ArrivingPro.ToString(),_game.ClientManager.ClientsByType(ProClientType.Instance).Count().ToString()}
            };

            ShowTable(clientsTable);
            Console.WriteLine();

            var menu = new MenuBuilder();
            return menu.AddItem("Построить что-нибудь", new ShowAction(new BuildView(_game)))
                .AddItem("Изменить рекламу", new ShowAction(new PromoView(_game)))
                .AddItem("Ждать конца дня", new NextTurnAction(_game))
                .Build().GetAction();
        }
        Game _game;
    }
}
