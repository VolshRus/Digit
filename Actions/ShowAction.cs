using System;
using NullGuard;
using Resort.Views;

namespace Resort.Actions
{
    class ShowAction : IAction
    {
        public ShowAction(IView view)
        {
            _shownView = view;
        }

        [return: AllowNull]
        public IAction Do()
        {
            return _shownView.GetAction();
        }

        IView _shownView;
    }
}
