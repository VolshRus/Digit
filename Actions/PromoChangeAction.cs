using System;
using Resort.Views;

namespace Resort.Actions
{
    class PromoChangeAction : IAction
    {
        public PromoChangeAction(Game game, int newPromoMultiplier)
        {
            _newPromoMultiplier = newPromoMultiplier;
            _game = game;
        }
        public IAction Do()
        {
            _game.PromoMultiplier = _newPromoMultiplier;
            return new ShowAction(new TurnStartView(_game));
        }

        private int _newPromoMultiplier;
        private Game _game;
    }
}
