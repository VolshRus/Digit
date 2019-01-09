using System;
using System.Collections.Generic;
using Resort.Actions;
using Resort.Types.Buildings;

namespace Resort.Views
{
    class BuildView : IView
    {
        public BuildView(Game game)
        {
            _game = game;
        }

        public IAction GetAction()
        {
            Console.Clear();
            Console.WriteLine("Сегодня еще можно построить зданий: " + _game.BuildManager.CanBuild);
            Console.WriteLine("Средств в наличии: " + _game.Money);
            Console.WriteLine();
            var menu = new MenuBuilder();
            _game.BuildManager.AvailableBuildings.ForEach(t => menu.AddItem(t.Description, new BuildAction(_game, t)));
            menu.AddItem("Вернуться в главное меню.", new ShowAction(new TurnStartView(_game)));
            return menu.Build().GetAction();
        }
        Game _game;
    }
}
