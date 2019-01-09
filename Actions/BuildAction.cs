using System;
using Resort.Types.Buildings;
using Resort.Views;

namespace Resort.Actions
{
    class BuildAction : IAction
    {
        public BuildAction(Game game, BuildType buildType)
        {
            _buildType = buildType;
            _game = game;
        }
        public IAction Do()
        {
            bool success = _game.TryBuild(_buildType);
            if (!success)
            {
                Console.WriteLine("Вы не можете сейчас построить это здание.");
                Console.WriteLine("Нажмите любую клавишу.");
                Console.ReadKey(true);
                return new ShowAction(new BuildView(_game));
            }
            else
            {
                return new ShowAction(new TurnStartView(_game));
            }
        }

        private BuildType _buildType;
        private Game _game;
    }

}
