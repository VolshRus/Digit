using System;
using System.Collections.Generic;
using Resort.Actions;

namespace Resort.Views
{
    class MenuBuilder
    {
        public MenuBuilder AddItem(string text, IAction action)
        {
            _list.Add((text, action));
            return this;
        }

        public MenuView Build()
        {
            return new MenuView(_list);
        }
        private List<(string, IAction)> _list = new List<(string, IAction)>();
    }
}
