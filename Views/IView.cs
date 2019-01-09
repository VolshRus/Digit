using System;
using Resort.Actions;

namespace Resort.Views
{
    interface IView
    {
        IAction GetAction();
    }
}
