using System;
using Resort.Views;

namespace Resort.Actions
{
    class NextTurnAction : IAction
    {
        public NextTurnAction(Game game)
        {
            _game = game;
        }

        public IAction Do()
        {
            _game.NewTurn();
            if (_game.IsWin || _game.IsLoose)
            {
                return new ShowAction(new GameoverView(_game));
            }
            return new ShowAction(new TurnStartView(_game));
        }

        private Game _game;
    }

}
