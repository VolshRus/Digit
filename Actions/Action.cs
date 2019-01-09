using System;
namespace Resort.Actions
{
    class Action : IAction
    {
        public IAction Do()
        {
            return new Action();
        }
    }
}
