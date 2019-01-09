using System;
using Resort.Actions;

namespace Resort.Views
{
    class PromoView : IView
    {
        public PromoView(Game game)
        {
            _game = game;
        }

        public IAction GetAction()
        {
            Console.Clear();
            Console.WriteLine("Сейчас на рекламу тратится: " + _game.PromoCost);
            Console.WriteLine("Это ежедневно меняет рейтинг на: " + _game.PromoRating);
            Console.WriteLine();
            var menu = new MenuBuilder();
            for (int i = 0; i < 6; i++)
            {
                menu.AddItem(i * _game.PromoStepCost, new PromoChangeAction(_game, i));
            }
            menu.AddItem("Вернуться в главное меню.", new ShowAction(new TurnStartView(_game)));
            return menu.Build().GetAction();
        }
        Game _game;
    }
}
