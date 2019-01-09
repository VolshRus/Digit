using System;
using NullGuard;
using Resort.Actions;

namespace Resort.Views
{
    class GameoverView : IView
    {
        public GameoverView(Game game)
        {
            _game = game;
        }

        [return: AllowNull]
        public IAction GetAction()
        {
            Console.Clear();
            if (_game.IsLoose)
            {
                Console.WriteLine("Игра закончена! Вы проиграли...");
            }
            else
            {
                Console.WriteLine("Поздравляю с победой!");
            }
            return null;
        }

        Game _game;

    }
}
